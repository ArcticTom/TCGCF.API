using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCGCF.API.Entities;
using TCGCF.API.Filters;
using TCGCF.API.Models;
using TCGCF.API.Services;

namespace TCGCF.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/deck")]
    [ValidateModel]
    [ApiVersion("0.1")] //api version supported
    //can have multiple
    //[ApiVersion("0.2")]
    public class DeckController : Controller
    {
        //log to console
        private ILogger<DeckController> _logger;
        private ICardInfoRepository _cardInfoRepository;

        public DeckController(ILogger<DeckController> logger, ICardInfoRepository cardInfoRepository)
        {
            _logger = logger;
            _cardInfoRepository = cardInfoRepository;
        }

        //get all decks
        [HttpGet()]
        public IActionResult GetDecks()
        {
            try
            {

                var deckEntities = _cardInfoRepository.GetDecks();

                var results = Mapper.Map<IEnumerable<DeckWithNoCardsDTO>>(deckEntities);

                return Ok(results);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetDecks.", ex);
                return StatusCode(500);
            }

        }

        /*
        //get all decks v2
        [HttpGet()]
        [MapToApiVersion("0.2")]
        public IActionResult GetDecks()
        {
            try
            {

                var deckEntities = _cardInfoRepository.GetDecks();

                var results = Mapper.Map<IEnumerable<DeckWithNoCardsDTOTest>>(deckEntities);

                return Ok(results);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetDecks.", ex);
                return StatusCode(500);
            }

        }
        */

        //get specific deck with id
        [HttpGet("{id}", Name = "GetDeck")]
        public IActionResult GetDeck(int id, bool includeCards = false)
        {
            try
            {

                var deck = _cardInfoRepository.GetDeck(id, includeCards);

                if (deck == null)
                {
                    return NotFound();
                }

                if (includeCards)
                {
                    var deckResult = Mapper.Map<DeckDTO>(deck);

                    return Ok(deckResult);
                }

                var deckWithNoCardsResult = Mapper.Map<DeckWithNoCardsDTO>(deck);

                return Ok(deckWithNoCardsResult);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetDeck with id {id}.", ex);
                return StatusCode(500);
            }

        }

        // create new deck
        [HttpPost()]
        public async Task<IActionResult> CreateDeck([FromBody] DeckAddingDTO deck)
        {
            try
            {

                if (deck == null)
                {
                    return BadRequest();
                }

                //Custom validation
                /*
                if (deck.Creator == deck.Name)
                {
                    ModelState.AddModelError("Creator", "Creator and Name cannot be the same.");
                }
                */

                var finalDeck = Mapper.Map<Deck>(deck);

                _cardInfoRepository.AddDeck(finalDeck);

                if (!(await _cardInfoRepository.SaveChanges()))
                {
                    return StatusCode(500, "Saving failed.");
                }

                var createdDeck = Mapper.Map<DeckDTO>(finalDeck);

                return CreatedAtRoute("GetDeck", new { id = createdDeck.Id }, createdDeck);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on CreateDeck.", ex);
                return StatusCode(500);
            }

        }

        //update deck with id
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDeck(int id, [FromBody] JsonPatchDocument<DeckUpdatingDTO> patchDoc)
        {
            try
            {

                if (patchDoc == null)
                {
                    return BadRequest();
                }

                var deckForUpdate = _cardInfoRepository.GetDeck(id, false);

                if (deckForUpdate == null)
                {
                    return NotFound();
                }

                var deckToPatch = Mapper.Map<DeckUpdatingDTO>(deckForUpdate);

                patchDoc.ApplyTo(deckToPatch, ModelState);

                //Custom validation
                /*
                if (deck.Creator == deck.Name)
                {
                    ModelState.AddModelError("Creator", "Creator and Name cannot be the same.");
                }
                */

                //try to validate patch
                TryValidateModel(deckToPatch);

                Mapper.Map(deckToPatch, deckForUpdate);

                if (!(await _cardInfoRepository.SaveChanges()))
                {
                    return StatusCode(500, "Saving failed.");
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on UpdateDeck with id {id}.", ex);
                return StatusCode(500);
            }

        }

        //delete deck with id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeck(int id)
        {
            try
            {

                var deckForDelete = _cardInfoRepository.GetDeck(id, false);

                if (deckForDelete == null)
                {
                    return NotFound();
                }

                _cardInfoRepository.DeleteDeck(deckForDelete);

                if (!(await _cardInfoRepository.SaveChanges()))
                {
                    return StatusCode(500, "Delete failed.");
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on DeleteDeck with setId {id}.", ex);
                return StatusCode(500);
            }

        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
    [ApiVersion("0.1")]     //api version supported
    public class DeckCardController : Controller
    {
        //log to console
        private ILogger<DeckCardController> _logger;
        private ICardInfoRepository _cardInfoRepository;
        private IMemoryCache _cache;

        public DeckCardController(ILogger<DeckCardController> logger, ICardInfoRepository cardInfoRepository, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
            _cardInfoRepository = cardInfoRepository;
        }

        //get all cards in deck with id
        [HttpGet("{deckId}/card")]
        public IActionResult GetCardsInDeck(int deckId)
        {
            try
            {

                if (!_cardInfoRepository.DeckExists(deckId))
                {
                    _logger.LogInformation($"Deck with id {deckId} was not found.");
                    return NotFound();
                }

                var cardsForDeck = _cardInfoRepository.GetCardsForDeck(deckId);

                var cardsForDeckResults = Mapper.Map<IEnumerable<CardDTO>>(cardsForDeck);

                return Ok(cardsForDeckResults);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetCardsInDeck with deckId {deckId}.", ex);
                return StatusCode(500);
            }
        }

        //get specific card in specific deck with ids
        [HttpGet("{deckId}/card/{cardId}", Name = "GetCardInDeck")]
        public IActionResult GetCardInDeck(int deckId, int cardId)
        {
            try
            {

                if (!_cardInfoRepository.DeckExists(deckId))
                {
                    _logger.LogInformation($"Deck with id {deckId} was not found.");
                    return NotFound();
                }

                var cardForDeck = _cardInfoRepository.GetCardForDeck(deckId, cardId);

                if (cardForDeck == null)
                {
                    _logger.LogInformation($"Card with id {cardId} and deckId {deckId} was not found.");
                    return NotFound();
                }

                var cardResult = Mapper.Map<CardDTO>(cardForDeck);

                return Ok(cardResult);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetCardInDeck with deckId {deckId} and cardId {cardId}.", ex);
                return StatusCode(500);
            }
        }

        //add a card to deck with id
        [HttpPost("{deckId}/card")]
        public async Task<IActionResult> AddCardToDeck(int deckId, [FromBody] CardToDeckAddingDTO card)
        {
            try
            {

                if (card == null)
                {
                    return BadRequest();
                }

                if (!_cardInfoRepository.DeckExists(deckId))
                {
                    return NotFound();
                }

                var finalCard = Mapper.Map<CardsInDeck>(card);

                _cardInfoRepository.AddCardToDeck(deckId, finalCard);

                if (!(await _cardInfoRepository.SaveChanges()))
                {
                    return StatusCode(500, "Saving failed.");
                }

                var cardForDeck = _cardInfoRepository.GetCardForDeck(deckId, finalCard.CardId);

                var createdCard = Mapper.Map<CardDTO>(cardForDeck);

                return CreatedAtRoute("GetCardInDeck", new { deckId = deckId, cardId = createdCard.Id }, createdCard);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on AddCardToDeck with deckId {deckId}.", ex);
                return StatusCode(500);
            }
        }

        //add a cards to deck with id
        [HttpPost("{deckId}/cards")]
        public async Task<IActionResult> AddCardsToDeck(int deckId, [FromBody] List<CardToDeckAddingDTO> cards)
        {
            try
            {

                if (cards == null)
                {
                    return BadRequest();
                }

                if (!_cardInfoRepository.DeckExists(deckId))
                {
                    return NotFound();
                }

                var finalCards = Mapper.Map<IEnumerable<CardsInDeck>>(cards);

                _cardInfoRepository.AddCardsToDeck(deckId, finalCards);

                if (!(await _cardInfoRepository.SaveChanges()))
                {
                    return StatusCode(500, "Saving failed.");
                }

                var cardsForDeck = _cardInfoRepository.GetCardsForDeck(deckId);

                var cardsForDeckResults = Mapper.Map<IEnumerable<CardDTO>>(cardsForDeck);

                return Ok(cardsForDeckResults);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on AddCardsToDeck with deckId {deckId}.", ex);
                return StatusCode(500);
            }
        }
    }
}

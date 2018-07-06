using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TCGCF.API.Filters;
using TCGCF.API.Models;
using TCGCF.API.Services;

namespace TCGCF.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/game")]
    [ValidateModel]
    [ApiVersion("0.1")]     //api version supported
    public class SetCardController : Controller
    {
        //log to console
        private ILogger<SetCardController> _logger;
        private ICardInfoRepository _cardInfoRepository;
        private IMemoryCache _cache;

        public SetCardController(ILogger<SetCardController> logger, ICardInfoRepository cardInfoRepository, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
            _cardInfoRepository = cardInfoRepository;
        }

        //get all cards from set
        [HttpGet("{abbr}/set/{setAbbr}/card")]
        public IActionResult GetCards(string abbr, string setAbbr)
        {
            try
            {

                if (!_cardInfoRepository.SetExists(abbr, setAbbr))
                {
                    _logger.LogInformation($"Set with abbr {abbr} and setAbbr {setAbbr} was not found.");
                    return NotFound();
                }

                var cardsForSet = _cardInfoRepository.GetCardsForSet(abbr, setAbbr);

                var cardsForSetResults = Mapper.Map<IEnumerable<CardNoIdDTO>>(cardsForSet);

                return Ok(cardsForSetResults);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetCards with abbr {abbr} and setAbbr {setAbbr}.", ex);
                return StatusCode(500);
            }
        }

        //get specific card from specific set with ids
        [HttpGet("{abbr}/set/{setAbbr}/card/{prtNum}")]
        public IActionResult GetCard(string abbr, string setAbbr, string prtNum)
        {
            try
            {
                if (!_cardInfoRepository.SetExists(abbr, setAbbr))
                {
                    _logger.LogInformation($"Set with abbr {abbr} and setAbbr {setAbbr} was not found.");
                    return NotFound();
                }

                var cardForSet = _cardInfoRepository.GetCardForSet(abbr, setAbbr, prtNum);

                if (cardForSet == null)
                {
                    _logger.LogInformation($"Card with prtNum {prtNum}, abbr {abbr} and setAbbr {setAbbr} was not found.");
                    return NotFound();
                }

                var cardResult = Mapper.Map<CardNoIdDTO>(cardForSet);

                return Ok(cardResult);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetCard with abbr {abbr}, setAbbr {setAbbr} and prtNum {prtNum}.", ex);
                return StatusCode(500);
            }
        }
    }
}

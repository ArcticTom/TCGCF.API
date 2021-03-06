﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TCGCF.API.Filters;
using TCGCF.API.Models;
using TCGCF.API.Services;

namespace TCGCF.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/game")]
    [ValidateModel]
    [ApiVersion("0.1")]     //api version supported
    public class SetController : Controller
    {
        //log to console
        private ILogger<SetController> _logger;
        private ICardInfoRepository _cardInfoRepository;

        public SetController(ILogger<SetController> logger, ICardInfoRepository cardInfoRepository)
        {
            _logger = logger;
            _cardInfoRepository = cardInfoRepository;
        }

        //gets all sets
        [HttpGet("{abbr}/set")]
        public IActionResult GetSets(string abbr)
        {
            try
            {
                var setEntities = _cardInfoRepository.GetSets(abbr);

                var results = Mapper.Map<IEnumerable<SetWithNoCardsDTO>>(setEntities);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception on GetSets.", ex);
                return StatusCode(500);
            }


        }

        //get specific set with abbr
        [HttpGet("{abbr}/set/{setAbbr}")]
        public IActionResult GetSet(string abbr, string setAbbr, bool includeCards = false)
        {
            try
            {

                var set = _cardInfoRepository.GetSet(abbr, setAbbr, includeCards);

                if (set == null)
                {
                    return NotFound();
                }

                if (includeCards)
                {
                    var setResult = Mapper.Map<SetDTO>(set);

                    return Ok(setResult);
                }

                var setwithNoCardsResult = Mapper.Map<SetWithNoCardsDTO>(set);

                return Ok(setwithNoCardsResult);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetSet with abbr {abbr}.", ex);
                return StatusCode(500);
            }
        }
    }
}

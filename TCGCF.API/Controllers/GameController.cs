using AutoMapper;
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
    public class GameController : Controller
    {
        //log to console
        private ILogger<SetController> _logger;
        private ICardInfoRepository _cardInfoRepository;

        public GameController(ILogger<SetController> logger, ICardInfoRepository cardInfoRepository)
        {
            _logger = logger;
            _cardInfoRepository = cardInfoRepository;
        }

        //gets all games
        [HttpGet()]
        public IActionResult GetGames()
        {
            try
            {
                var gameEntities = _cardInfoRepository.GetGames();

                var results = Mapper.Map<IEnumerable<GameDTO>>(gameEntities);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception on GetGames.", ex);
                return StatusCode(500);
            }


        }

        //get specific game with abbr
        [HttpGet("{abbr}")]
        public IActionResult GetGame(string abbr)
        {
            try
            {

                var game = _cardInfoRepository.GetGame(abbr);

                if (game == null)
                {
                    return NotFound();
                }

                var gameResult = Mapper.Map<GameDTO>(game);

                return Ok(gameResult);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception on GetGame with abbr {abbr}.", ex);
                return StatusCode(500);
            }
        }

    }
}

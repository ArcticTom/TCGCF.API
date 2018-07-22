using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCGCF.API.Filters;
using TCGCF.API.Models;
using TCGCF.API.Services;

namespace TCGCF.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/maintenance")]
    [ApiVersion("0.1")]     //api version supported
    public class MaintenanceController : Controller
    {
        //log to console
        private ILogger<SetController> _logger;
        private IConfiguration _configuration;

        public MaintenanceController(ILogger<SetController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        //updates card data
        [HttpGet("updatedb")]
        public IActionResult UpdateDB()
        {
            try
            {
                var task = DBUpdate.Begin(_configuration);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception on UpdateDB.", ex);
                return StatusCode(500);
            }


        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TCGCF.API.Entities;
using TCGCF.API.Filters;
using TCGCF.API.Models;

namespace TCGCF.API.Controllers
{
    public class AuthController : Controller
    {
        private CardInfoContext _context;
        private UserManager<User> _userMgr;
        private ILogger<AuthController> _logger;
        private IPasswordHasher<User> _hasher;
        private IHostingEnvironment _env;
        private IConfiguration _config;

        public AuthController(CardInfoContext context, UserManager<User> userMgr, ILogger<AuthController> logger, IPasswordHasher<User> hasher, IHostingEnvironment env, IConfiguration config)
        {
            _context = context;
            _userMgr = userMgr;
            _logger = logger;
            _hasher = hasher;
            _env = env;
            _config = config;
        }

        //to enable claims based authorization to specific methods
        //[Authorize(Policy = "SuperUsers")]
        //rate limit set to 50 requests in 5mins, can be modified via appSettings.json
        [ValidateModel]
        [HttpPost("api/auth/token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
        {
            try
            {
                //find user by username
                var user = await _userMgr.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    //check password
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaims = await _userMgr.GetClaimsAsync(user);

                        //supply token with claims
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
                        }.Union(userClaims);

                        //set security key
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["tokens:key"]));

                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        //create access token for 60mins
                        var token = new JwtSecurityToken(_config["tokens:issuer"], _config["tokens:audience"],
                            claims: claims, expires: DateTime.UtcNow.AddMinutes(60), signingCredentials: credentials
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }

                return StatusCode(400);

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Failed to generate authentication token.", ex);
                return StatusCode(500);
            }
        }

    }
}

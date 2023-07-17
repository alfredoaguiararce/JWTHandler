using JWT.Handler.Services;
using JWTSampleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using JWT.Handler.Models;
using System.Security.Claims;

namespace JWTSampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController: ControllerBase
    {
        private readonly IJWTHandler mJWTHandler;

        public LoginController(IJWTHandler mJWTHandler)
        {
            this.mJWTHandler = mJWTHandler;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            string User = loginDto.User;
            string Pass = loginDto.Password;

            Claim[] Claims = new Claim[]
            {
                new Claim("User", User),
                new Claim("Password", Pass)
            };

            JWTHandlerConfiguration configuration = new JWTHandlerConfiguration
            {
                Audience = "Your audience"
                                        ,
                Issuer = " Your Issuer"
                                        ,
                SecretKeySHA256 = "B221D9DBB083A7F33428D7C2A3C3198AE925614D70210E28716CCAA7CD4DDB79" // This is a random key in SHA-256
            };

            string JWT = mJWTHandler.GenerateJWT(configuration,
                        Claims
                        );

            return Ok(JWT);
        }

        [HttpGet("token/{JWT}")]
        public IActionResult IsJWTValid(string JWT)
        {
            JWTHandlerConfiguration configuration = new JWTHandlerConfiguration
            {
                Audience = "Your audience"
                            ,
                Issuer = " Your Issuer"
                            ,
                SecretKeySHA256 = "B221D9DBB083A7F33428D7C2A3C3198AE925614D70210E28716CCAA7CD4DDB79" // This is a random key in SHA-256
            };
            return Ok(mJWTHandler.ValidateToken(JWT, configuration));
        }

        [HttpGet("claims/token/{JWT}")]
        public IActionResult GetClaims(string JWT)
        {
            JWTHandlerConfiguration configuration = new JWTHandlerConfiguration
            {
                Audience = "Your audience"
                ,
                Issuer = " Your Issuer"
                ,
                SecretKeySHA256 = "B221D9DBB083A7F33428D7C2A3C3198AE925614D70210E28716CCAA7CD4DDB79" // This is a random key in SHA-256
            };
            return Ok(mJWTHandler.GetClaims(JWT, configuration));
        }
    }
}

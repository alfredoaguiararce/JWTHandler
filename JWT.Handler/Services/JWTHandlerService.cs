using JWT.Handler.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Handler.Services
{
    internal class JWTHandlerService : IJWTHandler
    {
        public JWTHandlerService()
        {
        }

        public string GenerateJWT(JWTHandlerConfiguration Configuration, Claim[] Claims, double MinutesDuration = 60)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.SecretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: Configuration.Issuer
                    ,audience: Configuration.Audience
                    ,claims: Claims
                    ,expires: DateTime.UtcNow.AddMinutes(MinutesDuration)
                    ,signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken()
        {
            throw new NotImplementedException();
        }
    }

    public interface IJWTHandler
    {
        string GenerateJWT(JWTHandlerConfiguration Configuration, Claim[] Claims, double MinutesDuration = 60);
        public bool ValidateToken();
    }
}

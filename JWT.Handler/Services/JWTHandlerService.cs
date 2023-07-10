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

/// <summary>
/// The function generates a JSON Web Token (JWT) using the provided configuration, claims, and
/// duration.
/// </summary>
/// <param name="JWTHandlerConfiguration">JWTHandlerConfiguration is a class that contains the
/// configuration settings for generating the JWT token. It likely includes properties such as
/// SecretKey, Issuer, and Audience.</param>
/// <param name="Claims">The `Claims` parameter is an array of `Claim` objects. A `Claim` represents a
/// statement about a subject and can contain information such as the subject's identity, role, or any
/// other relevant information. These claims will be included in the generated JWT token.</param>
/// <param name="MinutesDuration">The MinutesDuration parameter is an optional parameter that specifies
/// the duration of the JWT token in minutes. By default, it is set to 60 minutes.</param>
/// <returns>
/// The method returns a string representation of a JSON Web Token (JWT).
/// </returns>
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

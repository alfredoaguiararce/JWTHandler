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
        private string mSecretKeySHA256;
        private string mIssuer;
        private string mAudience;

        public JWTHandlerService(){}

        public string GenerateJWT(JWTHandlerConfiguration Configuration, Claim[] Claims, double MinutesDuration = 60)
        {
            this.mAudience = Configuration.Audience;
            this.mIssuer = Configuration.Issuer;
            this.mSecretKeySHA256 = Configuration.SecretKeySHA256;

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.SecretKeySHA256));
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

        
        public ClaimsPrincipal? GetClaims(string JWT)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = mIssuer,
                ValidAudience = mAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mSecretKeySHA256))
            };

            try
            {
                SecurityToken validateToken;
                return tokenHandler.ValidateToken(JWT, validationParameters, out validateToken);
                
            }
            catch (Exception)
            {
                // Token inválido
                return null;
            }
        }


        public bool ValidateToken(string JWT)
        {
            try
            {
                ClaimsPrincipal? claims = GetClaims(JWT);
                if (claims is null) return false;
                return true;
            }
            catch (Exception)
            {
                // Token inválido
                return false;
            }
            
        }
    }

    public interface IJWTHandler
    {
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
        public string GenerateJWT(JWTHandlerConfiguration Configuration, Claim[] Claims, double MinutesDuration = 60);
        /// <summary>
        /// The function `ValidateToken` checks if a given JWT (JSON Web Token) is valid by attempting to
        /// retrieve claims from it.
        /// </summary>
        /// <param name="JWT">The JWT parameter is a string that represents a JSON Web Token.</param>
        /// <returns>
        /// The method returns a boolean value.
        /// </returns>
        public bool ValidateToken(string JWT);
        /// <summary>
        /// The function `GetClaims` takes a JWT (JSON Web Token) as input and returns a
        /// `ClaimsPrincipal` object if the token is valid, otherwise it returns null.
        /// </summary>
        /// <param name="JWT">The JWT parameter is the JSON Web Token that you want to validate and
        /// extract claims from.</param>
        /// <returns>
        /// The method is returning a ClaimsPrincipal object.
        /// </returns>
        public ClaimsPrincipal? GetClaims(string JWT);
    }
}

using System.ComponentModel.DataAnnotations;

namespace JWT.Handler.Models
{
    public class JWTHandlerConfiguration
    {

        public JWTHandlerConfiguration(string secretKey, string issuer, string audience)
        {
            SecretKey = secretKey;
            Issuer = issuer;
            Audience = audience;
        }

        [Required]
        public string SecretKey { get; set; }
        [Required]
        public string Issuer { get; set; }
        [Required]
        public string Audience { get; set; }

    }
}

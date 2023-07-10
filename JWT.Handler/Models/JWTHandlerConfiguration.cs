using System.ComponentModel.DataAnnotations;

namespace JWT.Handler.Models
{
    public class JWTHandlerConfiguration
    {
        [Required]
        [MaxLength(64)]
        public string SecretKeySHA256 { get; set; }
        [Required]
        public string Issuer { get; set; }
        [Required]
        public string Audience { get; set; }

    }
}

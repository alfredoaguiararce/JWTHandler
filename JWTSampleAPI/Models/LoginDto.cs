using System.ComponentModel.DataAnnotations;

namespace JWTSampleAPI.Models
{
    public class LoginDto
    {
        [Required]
        public string User { get; set; }   
        [Required]
        public string Password { get; set; }
    }
}

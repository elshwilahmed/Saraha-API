using System.ComponentModel.DataAnnotations;

namespace SarahaAPI.DTOs
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, StringLength(30, MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
}

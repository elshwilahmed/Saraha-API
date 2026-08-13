using System.ComponentModel.DataAnnotations;

namespace SarahaAPI.DTOs
{
    public class MessageCreate
    {
        [Required(ErrorMessage = "ResieverID is required")]
        public int RecieverID { get; set; }

        [Required(ErrorMessage = "The Content is required")]
        public string Content { get; set; } = null!;
    }
}

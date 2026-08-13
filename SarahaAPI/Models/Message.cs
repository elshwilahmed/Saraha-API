using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SarahaAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required, MaxLength(1000)]
        public string Content { get; set; } = null!;

        public DateOnly? MessageDate { get; set; }

        public TimeOnly? MessageTime { get; set; }

        public int ReceiverId { get; set; }
        [ForeignKey(nameof(ReceiverId))]
        public User user { get; set; }
    }
}

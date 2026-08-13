using SarahaAPI.Models;

namespace SarahaAPI.DTOs
{
    public class UserResponse
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public List<MessageResponse>? Messages { get; set; }
    }
}

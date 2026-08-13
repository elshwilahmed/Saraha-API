using SarahaAPI.Models;

namespace SarahaAPI.Services
{
    public interface IJWTService
    {
        public string GenerateToken(User user);
    }
}

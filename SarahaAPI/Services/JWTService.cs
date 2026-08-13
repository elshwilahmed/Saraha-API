using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SarahaAPI.Models;

namespace SarahaAPI.Services
{
    public class JWTService : IJWTService
    {
        readonly IConfiguration _config;

        public JWTService(IConfiguration configuration)
        {
            _config = configuration;
        }


        public string GenerateToken(User user)
        {
            var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var creds = new SigningCredentials(
                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"])), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
               issuer: _config["JWT:Issuer"],
               audience: _config["JWT:Audience"],
               claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddDays(1)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

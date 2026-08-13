using SarahaAPI.DTOs;
using SarahaAPI.Models;

namespace SarahaAPI.Services
{
    public interface IUserService
    {

        public UserResponse AddUser(UserCreate user);
        public UserResponse UpdateUser(UserCreate user);
        public UserResponse DeleteUserById(int id);
        public List<UserResponse> GetUsers();
        public UserResponse GetUserById(int id, int loggedID);
        public string Register(UserCreate user);
        public string Login(LoginDto user);

    }
}

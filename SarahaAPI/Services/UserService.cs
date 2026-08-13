using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SarahaAPI.Data;
using SarahaAPI.DTOs;
using SarahaAPI.Models;

namespace SarahaAPI.Services
{
    public class UserService : IUserService
    {
        readonly SarahaDbContext _db;
        readonly IJWTService _jWTService;
        public UserService(SarahaDbContext db, IJWTService jWTService)
        {
            _db = db;
            _jWTService = jWTService;
        }

        public UserResponse AddUser(UserCreate user)
        {
            var Exists = _db.Users.FirstOrDefault(u => u.Email == user.Email);

            if (Exists != null)
            {
                return null!;
            }

            User u = new User
            {
                Email = user.Email,
                UserName = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            _db.Users.Add(u);
            _db.SaveChanges();

            return new UserResponse
            {
                Email = u.Email,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            };
        }

        public UserResponse DeleteUserById(int id)
        {
            var u = _db.Users.FirstOrDefault(u => u.Id == id);

            if (u == null)
                return null!;

            _db.Users.Remove(u);
            _db.SaveChanges();

            return new UserResponse
            {
                Email = u.Email,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            };
        }

        public UserResponse GetUserById(int id, int loggedID)
        {

            if (loggedID == id)
            {
                return _db.Users.Where(u => u.Id == id)
                                .Select(user => new UserResponse
                                {
                                    Email = user.Email,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    FullName = user.FullName,
                                    UserName = user.UserName,
                                    Messages = user.Messages.Select(m => new MessageResponse
                                    {
                                        Content = m.Content,
                                        msgTime = m.MessageTime
                                    }).ToList()
                                }).FirstOrDefault()!;
            }

            return _db.Users.Where(u => u.Id == id)
                                .Select(user => new UserResponse
                                {
                                    Email = user.Email,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    FullName = user.FullName,
                                    UserName = user.UserName,

                                }).FirstOrDefault()!;
        }
        public List<UserResponse> GetUsers()
        {
            return _db.Users.Select(u => new UserResponse
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                FullName = u.FullName,
                UserName = u.UserName,
                
            }).ToList();
        }

        public string Login(LoginDto user)
        {
            var u = _db.Users.FirstOrDefault(u => u.Email == user.Email);

            if (u == null)
                return null!;

            var verified = BCrypt.Net.BCrypt.Verify(user.Password, u.PasswordHash);

            if(!verified)
                return null!;

            var token = _jWTService.GenerateToken(u);

            return token;

        }

        public string Register(UserCreate user)
        {
            var u = _db.Users.FirstOrDefault(u=>u.Email == user.Email);

            if (u != null)
                return null!;

            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return "registered successfully";
        }

        public UserResponse UpdateUser(UserCreate user)
        {
            User? dbuser = _db.Users
                .Include(u => u.Messages)
                .FirstOrDefault(u=>u.Email == user.Email);

            if (dbuser == null) return null!;

            dbuser.FirstName = user.FirstName;
            dbuser.LastName = user.LastName;
            dbuser.UserName = user.Username;
            dbuser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _db.SaveChanges();

            return new UserResponse
            {
                Email = dbuser.Email,
                UserName = dbuser.UserName,
                FirstName = dbuser.FirstName,
                LastName = dbuser.LastName,
                FullName = dbuser.FullName,
                Messages = dbuser.Messages.Select(m=>new MessageResponse
                {
                    Content=m.Content,
                    msgTime = m.MessageTime
                    
                }).ToList()
            };
        }
    }
}


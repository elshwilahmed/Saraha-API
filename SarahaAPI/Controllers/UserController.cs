using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SarahaAPI.DTOs;
using SarahaAPI.Models;
using SarahaAPI.Responses;
using SarahaAPI.Services;

namespace SarahaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _service;
        public UserController(IUserService userService)
        {
            _service = userService;
        }
        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult DeleteUserById(int id)
        {
            var ID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if(ID != id) 
                return Unauthorized("You can delete your account only!!");

            var user = _service.DeleteUserById(id);

            if (user == null)
                return NotFound(APIResponse<UserResponse>.Failure("This User is not Found"));

            return Ok(APIResponse<UserResponse>.Success(user, "The User was deleted"));
        }

        [HttpGet("All")]
        public ActionResult GetUsers()
        {
            return _service.GetUsers() == null ?
                NotFound(APIResponse<List<UserResponse>>.Failure("This User is not Found")) :
                Ok(APIResponse<List<UserResponse>>.Success(_service.GetUsers()));
        }

        [HttpGet("{id:int}")]
        public ActionResult GetUser(int id)
        {
            int loggedID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var user = _service.GetUserById(id, loggedID);

            if (user == null)
                return NotFound(APIResponse<UserResponse>.Failure("This User is not Found"));

            return Ok(APIResponse<UserResponse>.Success(user));
        }

        [HttpPut]
        [Authorize]
        public ActionResult UpdateUser(UserCreate user)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email != user.Email)
                return Unauthorized("You can update your account only!!");

            var u = _service.UpdateUser(user);

            if (u == null) return NotFound(APIResponse<UserResponse>.Failure("This User is not Found"));

            return Ok(APIResponse<UserResponse>.Success(u, "The User was updated successfully"));
        }
        [HttpPost("Login")]
        public ActionResult Login(LoginDto user)
        {
            var u = _service.Login(user);

            if(u is null) return Unauthorized(APIResponse<string>.Failure("Invalid Email or password"));

            return Ok(APIResponse<string>.Success(u));
        }

        [HttpPost("Register")]
        public ActionResult Register(UserCreate user)
        {
           var u = _service.Register(user);

            if (u is null) return BadRequest(APIResponse<string>.Failure("This email allready exists"));

            return Ok(APIResponse<string>.Success(u));
        }
    } 
}

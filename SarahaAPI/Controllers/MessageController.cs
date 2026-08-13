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
    public class MessageController : ControllerBase
    {
        readonly IMessageService _msgService;
        public MessageController(IMessageService _msgService)
        {
            this._msgService = _msgService;
        }
        [HttpGet("All")]
        [Authorize]
        public ActionResult GetAllMessages()
        {
            return Ok(APIResponse<List<MessageResponse>>.Success(_msgService.GetMessages()));
        }

        [HttpGet("user{id:int}")]
        [Authorize]
        public ActionResult GetUserMessagesById(int id)
        {

            var ID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (ID != id)
                return Unauthorized("You can Get your messages only!!");

            var messages = _msgService.GetUserMessagesById(id);

            if (messages == null)
                return NotFound(APIResponse<MessageResponse>.Failure("This Message is not Found"));

            return Ok(APIResponse<List<MessageResponse>>.Success(messages));
        }

        [HttpPost("send")]
        public ActionResult AddMessage([FromBody] MessageCreate message)
        {
            return Ok(APIResponse<MessageResponse>.Success(_msgService.AddMessage(message)));
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult UpdateMessagesById(int id, MessageUpdate newMessage)
        {

            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);


            MessageResponse msg = _msgService.UpdateMessagesById(id, newMessage, UserID);

            if (msg == null)
            {
                return NotFound(APIResponse<MessageResponse>.Failure("This Message is not Found or you don't have permission")); 
            }

            return Ok(APIResponse<MessageResponse>.Success(msg, "Message Was Updated Successfully"));
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult DeleteMessagesById(int id)
        {

            var UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            MessageResponse msg = _msgService.DeleteMessagesById(id, UserID);
            if (msg == null)
            {
                return NotFound(APIResponse<MessageResponse>.Failure("This Message is not Found or you don't have permission"));
            }

            return Ok(APIResponse<MessageResponse>.Success(msg, "Message Was Deleted Successfully"));
        }
    }
}


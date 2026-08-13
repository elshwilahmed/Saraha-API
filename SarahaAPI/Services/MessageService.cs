using System.Security.Claims;
using SarahaAPI.Data;
using SarahaAPI.DTOs;
using SarahaAPI.Models;

namespace SarahaAPI.Services
{
    public class MessageService : IMessageService 
    {
        readonly SarahaDbContext _db;
        public MessageService(SarahaDbContext DB)
        {
            _db = DB;
        }

        public MessageResponse AddMessage(MessageCreate message)
        {
            var NewMessage = new Message
            {
                Content = message.Content,
                ReceiverId = message.RecieverID,
                MessageTime = TimeOnly.FromDateTime(DateTime.Now),
                MessageDate = DateOnly.FromDateTime(DateTime.Now),
            };

            _db.Messages.Add(NewMessage);
            _db.SaveChanges();

            return new MessageResponse
            {
                Content = NewMessage.Content,
                msgTime = NewMessage.MessageTime
            };
        }

        public MessageResponse DeleteMessagesById(int id, int userID)
        {
            Message msg = _db.Messages.Find(id);

            if (msg == null) return null!;
            if (userID != msg.ReceiverId)
                return null!;

            _db.Messages.Remove(msg);
            _db.SaveChanges();

            return new MessageResponse
            {
                msgTime = msg.MessageTime,
                Content = msg.Content
            };
        }

        public List<MessageResponse> GetMessages()
        {
            return _db.Messages.Select(m => new MessageResponse
            {
                msgTime = m.MessageTime,
                Content = m.Content
            }).ToList();
        }

        public List<MessageResponse> GetUserMessagesById(int id)
        {
            var messages = _db.Messages.Where(m => m.ReceiverId == id);

            if (messages is null) return null!;

            return messages.Select(m => new MessageResponse
            {
                msgTime = m.MessageTime,
                Content = m.Content
            }).ToList();
        }

        public MessageResponse UpdateMessagesById(int id, MessageUpdate newMessage, int userID)
        {
           
            Message msg = _db.Messages.Find(id);

            if (msg == null) return null!;

            if (userID != msg.ReceiverId)
                return null!;

            msg.Content = newMessage.Content;
            _db.SaveChanges();
            return new MessageResponse
            {
                msgTime = TimeOnly.FromDateTime(DateTime.Now),
                Content = msg.Content
            };
        }
    }
}


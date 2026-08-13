using SarahaAPI.DTOs;

namespace SarahaAPI.Services
{
    public interface IMessageService
    {
        List<MessageResponse> GetMessages();

        List<MessageResponse> GetUserMessagesById(int id);

        MessageResponse AddMessage(MessageCreate message);

        MessageResponse UpdateMessagesById(int id, MessageUpdate newMessage, int userID);

        MessageResponse DeleteMessagesById(int id, int userID);
    }
}

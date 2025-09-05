using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllMessages();
        Task<Message> GetMessageById(int id);
        Task AddMessage(Message Message);
        Task UpdateMessage(Message Message);
        Task DeleteMessage(int id);
        Task<List<Message>> GetMessageByReceiverIdAndSenderId(string userId1, string userId2);
        Task<List<Message>> GetMessageLastest(string userId1, string userId2);
    }
}

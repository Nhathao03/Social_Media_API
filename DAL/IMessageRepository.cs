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
        Task<List<Message>> GetMessageByReceiverID(string ReceiverID, string SenderID);
    }
}

using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllMessageAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task AddMessageAsync(MessageDTO messageDTO);
        Task UpdateMessageAsync(Message Message);
        Task DeleteMessageAsync(int id);
        Task<List<Message>> GetMessageByReceiverIdAndSenderIdAsync(string userId1, string userId2);
        Task<List<Message>> GetMessageLastestAsync(string userId1, string userId2);
    }
}

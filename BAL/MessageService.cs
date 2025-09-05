using Microsoft.AspNetCore.SignalR;
using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class MessageService : IMessageService
    {
        private readonly MessageRepository _MessageRepository;

        public MessageService(MessageRepository repository)
        {
            _MessageRepository = repository;
        }

        public async Task<IEnumerable<Message>> GetAllMessageAsync()
        {
            return await _MessageRepository.GetAllMessages();
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _MessageRepository.GetMessageById(id);
        }

        public async Task AddMessageAsync(MessageDTO messageDTO)
        {
            var message = new Message
            {
                SenderId = messageDTO.SenderID,
                ReceiverId = messageDTO.ReceiverID,
                Content = messageDTO.Content,
                CreatedAt = DateTime.UtcNow,
            };

            await _MessageRepository.AddMessage(message);
        }

        public async Task UpdateMessageAsync(Message Message)
        {
            await _MessageRepository.UpdateMessage(Message);
        }

        public async Task DeleteMessageAsync(int id)
        {
            await _MessageRepository.DeleteMessage(id);
        } 

        public async Task<List<Message>> GetMessageByReceiverIdAndSenderIdAsync(string userId1, string userId2)
        {
            return await _MessageRepository.GetMessageByReceiverIdAndSenderId(userId1, userId2);
        }

        public async Task<List<Message>> GetMessageLastestAsync(string userId1, string userId2)
        {
            return await _MessageRepository.GetMessageLastest(userId1, userId2);
        }
    }
}

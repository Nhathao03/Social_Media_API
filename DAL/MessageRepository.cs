using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _context.messages.ToListAsync();
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _context.messages.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddMessage(Message Message)
        {          
            _context.AddAsync(Message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMessage(Message Message)
        {
            _context.messages.Update(Message);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessage(int id)
        {
            var Message = await _context.messages.FindAsync(id);
            if (Message != null)
            {
                _context.messages.Remove(Message);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetMessageByReceiverID (string ReceiverID, string SenderID)
        {
            return await _context.messages.Where(m => (m.ReceiverId == ReceiverID && m.SenderId == SenderID) 
                                                || (m.ReceiverId == SenderID && m.SenderId == ReceiverID))
                                                .OrderBy(m => m.CreatedAt).ToListAsync();
        }

    }
}

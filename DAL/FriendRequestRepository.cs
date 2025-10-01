using Microsoft.EntityFrameworkCore;
using Social_Media.Helpers;
using Social_Media.Models;
using Social_Media.Models.DTO;
using static Social_Media.Helpers.Constants;
namespace Social_Media.DAL
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly AppDbContext _context;

        public FriendRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FriendRequest>> GetAllFriendRequests()
        {
            return await _context.friendRequests.ToListAsync();
        }

        public async Task<FriendRequest> GetFriendRequestById(int id)
        {
            return await _context.friendRequests.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<IEnumerable<FriendRequest>> GetFriendRequestByUserID(string id)
        {
            return await _context.friendRequests.Where(f => f.SenderID == id || f.ReceiverID == id).ToListAsync();
        }
        public async Task<IEnumerable<FriendRequest>> GetFriendRequestByReceiverID(string userId)
        {
            return await _context.friendRequests.Where(f => f.ReceiverID == userId && f.status == 1).ToListAsync();
        }

        public async Task AddFriendRequest(FriendRequest friendRequest)
        {
            _context.friendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFriendRequest(FriendRequest FriendRequest)
        {
            _context.friendRequests.Update(FriendRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFriendRequest(int id)
        {
            var FriendRequest = await _context.friendRequests.FindAsync(id);
            if (FriendRequest != null)
            {
                _context.friendRequests.Remove(FriendRequest);
                await _context.SaveChangesAsync();
            }
        }
    }
}

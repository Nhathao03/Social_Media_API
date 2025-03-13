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

        public async Task<List<FriendRequest>> GetFriendRequestBySenderID(string id)
        {
            return await _context.friendRequests.Where(f => f.SenderID == id).ToListAsync();
        }

        public async Task AddFriendRequest(FriendRequestDTO friendRequestDTO)
        {
            var request = new FriendRequest
            {
                SenderID = friendRequestDTO.SenderID,
                ReceiverID = friendRequestDTO.ReceiverID,
                status = (int)Constants.FriendRequestEnum.Pending,
            };
            _context.friendRequests.Add(request);
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

        //REVISE THIS CODE
        public async Task ConfirmRequest(int id)
        {
            var request = await _context.friendRequests.FindAsync(id);
            if (request == null)
            {
                throw new KeyNotFoundException($"Friend request with ID {id} not found.");
            }
            request.status = (int)Constants.FriendRequestEnum.Accepted;
            await _context.SaveChangesAsync();
        }

    }
}

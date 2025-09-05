using Microsoft.EntityFrameworkCore;
using Social_Media.Helpers;
using Social_Media.Models;
using Social_Media.Models.DTO;
using static Social_Media.Helpers.Constants;
namespace Social_Media.DAL
{
    public class FriendRepository : IFriendRepository
    {
        private readonly AppDbContext _context;

        public FriendRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Friends>> GetAllFriends()
        {
            return await _context.friends.ToListAsync();
        }

        public async Task<Friends> GetFriendById(int id)
        {
            return await _context.friends.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddFriend(Friends friends)
        {
            _context.friends.Add(friends);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFriend(Friends friends)
        {
            _context.friends.Update(friends);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFriend(int id)
        {
            var Friend = await _context.friends.FindAsync(id);
            if (Friend != null)
            {
                _context.friends.Remove(Friend);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Friends>> GetFriendsByUserID( string userID)
        {
            return await _context.friends.Where(f => f.UserID == userID).ToListAsync();
        }

        public async Task<List<Friends>> getFriendRecentlyAdded(string userID)
        {
            var threeDaysAgo = DateTime.UtcNow.AddDays(-3);
            return await _context.friends
                .Where(f => f.UserID == userID && f.CreatedDate >= threeDaysAgo || f.FriendID == userID && f.CreatedDate >= threeDaysAgo)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<string>> GetFriendOfEachUser(string userID)
        {
            return await _context.friends
                .Where(f => f.UserID == userID || f.FriendID == userID)
                .Select(f => f.UserID == userID ? f.FriendID : f.UserID)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Friends>> GetFriendBaseOnHomeTown(string userId)
        {
            var currentUser = await _context.users.FirstOrDefaultAsync(u => u.Id == userId);
            if (currentUser == null) return null;

            var usersWithSameAddress = await _context.users
                .Where(u => u.addressID == currentUser.addressID && u.Id != userId)
                .ToListAsync();

            if (usersWithSameAddress == null || !usersWithSameAddress.Any()) return new List<Friends>();

            var result = new List<Friends>();

            foreach (var otherUser in usersWithSameAddress)
            {
                var friend = await _context.friends
                    .FirstOrDefaultAsync(f =>
                        (f.UserID == userId && f.FriendID == otherUser.Id) ||
                        (f.UserID == otherUser.Id && f.FriendID == userId));

                if (friend != null)
                {
                    result.Add(friend);  
                }
            }

            return result;
        }
    }
}

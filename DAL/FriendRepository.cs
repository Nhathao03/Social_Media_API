﻿using Microsoft.EntityFrameworkCore;
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
    }
}

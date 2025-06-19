using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _context;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Like>> GetAllLikes()
        {
            return await _context.likes.ToListAsync();
        }

        public async Task<Like> GetLikeById(int id)
        {
            return await _context.likes.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddLike(Like like)
        {          
            _context.AddAsync(like);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLike(Like like)
        {
            _context.likes.Update(like);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLike(int id)
        {
            var like = await _context.likes.FindAsync(id);
            if (like != null)
            {
                _context.likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Like?> GetLikeByUserAndPost(string userId, int postId)
        {
            return await _context.likes.FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);
        }

        public async Task<IEnumerable<Like>> GetLikesByPostId(int postId)
        {
            return await _context.likes.Where(l => l.PostId == postId).ToListAsync();
        }

    }
}

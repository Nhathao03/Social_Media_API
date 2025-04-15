using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.DAL
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPost()
        {
            return await _context.posts.Include(p => p.Comments).Include(p => p.Likes).Include(p => p.PostCategory).Include(p => p.PostImages).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostNearestCreatedAt()
        {
            return await _context.posts.Where(p => p.CreatedAt <= DateTime.UtcNow)
                                 .Include(p => p.Comments)
                                 .Include(p => p.Likes)
                                 .Include(p => p.PostCategory)
                                 .Include(p => p.PostImages)
                                 .OrderByDescending(p => p.CreatedAt).ToListAsync();
        }
        public async Task<Post> GetPostById(int id)
        {
            return await _context.posts.Include(p => p.Comments).Include(p => p.Likes).Include(p => p.PostCategory).Include(p => p.PostImages).FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddPost(Post post)
        {
            _context.posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _context.posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var post = await _context.posts.FindAsync(id);
            if (post != null)
            {
                _context.posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        //Get posts by userID
        public async Task<IEnumerable<Post>> GetPostsByUserID(string userID)
        {
            return await _context.posts.Include(p => p.Comments).Include(p => p.Likes).Include(p => p.PostCategory).Include(p => p.PostImages).Where(p => p.UserID == userID).ToListAsync();
        }

    }
}

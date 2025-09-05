using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.DAL
{
    public class PostImageRepository : IPostImageRepository
    {
        private readonly AppDbContext _context;

        public PostImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostImage>> GetAllPostImage()
        {
            return await _context.post_image.ToListAsync();
        }

        public async Task<PostImage> GetPostImageById(int id)
        {
            return await _context.post_image.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddPostImage(List<PostImage> postImages)
        {
            _context.post_image.AddRange(postImages);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostImage(PostImage postImage)
        {
            _context.post_image.Update(postImage);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostImage(int id)
        {
            var postImage = await _context.post_image.FindAsync(id);
            if (postImage != null)
            {
                _context.post_image.Remove(postImage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<PostImage>> GetAllPostImagesByUserID(string userId)
        {
            var postImages = await _context.post_image.Where(pi => _context.posts.Where(p => p.UserID == userId)
                .Select(p => p.ID)
                .Contains(pi.PostId))
                .ToListAsync();

            return postImages;
        }

        public async Task<List<PostImage>> GetPostImagesByPostID(int postId)
        {
            return await _context.post_image.Where(pi => pi.PostId == postId).ToListAsync();
        }
    }
}

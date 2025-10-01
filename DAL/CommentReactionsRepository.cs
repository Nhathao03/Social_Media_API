using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.DAL
{
    public class CommentReactionsRepository : ICommentReactionsRepository
    {
        private readonly AppDbContext _context;

        public CommentReactionsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentReactions>> GetAllCommentReactions()
        {
            return await _context.commentReactions.ToListAsync();
        }

        public async Task<CommentReactions> GetCommentReactionsById(int id)
        {
            return await _context.commentReactions.FindAsync(id);
        }

        public async Task AddNewCommentReactions(CommentReactions commentReactions)
        {
            _context.commentReactions.Add(commentReactions);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentReactions(CommentReactions commentReactions)
        {
            _context.commentReactions.Update(commentReactions);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentReactionsById(int id)
        {
            var commentReactions = await _context.commentReactions.FindAsync(id);
            if (commentReactions != null)
            {
                _context.commentReactions.Remove(commentReactions);
                await _context.SaveChangesAsync();
            }
        }
    }
}

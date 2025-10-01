using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface ICommentRepliesRepository
    {
        Task<IEnumerable<CommentReplies>> GetAllCommentReplies();
        Task<CommentReplies> GetCommentRepliesById(int id);
        Task UpdateCommentReplies(CommentReplies commentReplies);
        Task DeleteCommentReplies(int id);
        Task<int> AddNewCommentReplies(CommentReplies commentReplies);
    }
}

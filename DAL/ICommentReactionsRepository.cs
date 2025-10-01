using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface ICommentReactionsRepository
    {
        Task<IEnumerable<CommentReactions>> GetAllCommentReactions();
        Task<CommentReactions> GetCommentReactionsById(int id);
        Task UpdateCommentReactions(CommentReactions commentReactions);
        Task AddNewCommentReactions(CommentReactions commentReactions);
        Task DeleteCommentReactionsById(int id);
    }
}

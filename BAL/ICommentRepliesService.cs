using Social_Media.Models;
using Social_Media.Models.DTO.Comment;

namespace Social_Media.BAL
{
    public interface ICommentRepliesService
    {
        Task<IEnumerable<CommentReplies>> GetAllCommentReplies();
        Task<CommentReplies> GetCommentRepliesById(int id);
        Task UpdateCommentReplies(CommentRepliesDTO model);
        Task DeleteCommentReplies(int id);
        Task<int> AddCommentReplies(CommentRepliesDTO model);
    }
}

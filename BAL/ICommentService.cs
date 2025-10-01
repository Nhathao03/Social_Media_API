using Social_Media.Models;
using Social_Media.Models.DTO.Comment;

namespace Social_Media.BAL
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentByPostIDAsync(int postID);
        Task<int> AddCommentAsync(CommentDTO comment);
        Task UpdateCommentAsync(CommentDTO comment);
        Task DeleteCommentAsync(int id);
        Task LikeCommentAsync(int commentId, string userId);
    }
}

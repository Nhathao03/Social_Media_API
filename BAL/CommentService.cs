using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO.Comment;

namespace Social_Media.BAL
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository repository)
        {
            _commentRepository = repository;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllComments();
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _commentRepository.GetCommentById(id);
        }
        public async Task<IEnumerable<Comment>> GetCommentByPostIDAsync(int postID)
        {
            return await _commentRepository.GetCommentByPostID(postID);
        }

        public async Task<int> AddCommentAsync(CommentDTO commentDTO)
        {
            if (commentDTO == null)
                throw new ArgumentNullException(nameof(commentDTO));
            if (string.IsNullOrWhiteSpace(commentDTO.Content))
                throw new ArgumentException("Comment content cannot be empty.", nameof(commentDTO.Content));

            var comment = new Comment
            {
                UserId = commentDTO.userID,
                PostId = commentDTO.postID,         
                Content = commentDTO.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Image = commentDTO.ImageUrl,
            };

            var newId = await _commentRepository.AddComment(comment);
            return newId;
        }

        public async Task UpdateCommentAsync(CommentDTO commentDTO)
        {
            var comment = await _commentRepository.GetCommentById(commentDTO.Id);
            comment.Content = commentDTO.Content;
            comment.UpdatedAt = DateTime.UtcNow;
            comment.Image = commentDTO.ImageUrl;
            await _commentRepository.UpdateComment(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _commentRepository.DeleteComment(id);
        }

        public async Task LikeCommentAsync(int commentId, string userId)
        {
            
        }
    }
}

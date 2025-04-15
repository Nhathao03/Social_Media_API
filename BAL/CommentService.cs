using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class CommentService : ICommentService
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(CommentRepository repository)
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

        public async Task AddCommentAsync(CommentDTO commentDTO)
        {
            var comment = new Comment
            {
                UserId = commentDTO.userID,
                PostId = commentDTO.postID,
                Sticker = commentDTO.sticker,
                Content = commentDTO.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Image = commentDTO.ImageUrl,
            };

            await _commentRepository.AddComment(comment);
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            await _commentRepository.UpdateComment(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _commentRepository.DeleteComment(id);
        }

    }
}

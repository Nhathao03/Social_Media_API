using NuGet.Protocol.Core.Types;
using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO.Comment;

namespace Social_Media.BAL
{
    public class CommentRepliesService : ICommentRepliesService
    {
        private readonly ICommentRepliesRepository _commentRepliesRepository;

        public CommentRepliesService(ICommentRepliesRepository commentRepliesRepository)
        {
            _commentRepliesRepository = commentRepliesRepository;
        }

        public async Task<IEnumerable<CommentReplies>> GetAllCommentReplies()
        {
            return await _commentRepliesRepository.GetAllCommentReplies();
        }

        public async Task<CommentReplies> GetCommentRepliesById(int id)
        {
            return await _commentRepliesRepository.GetCommentRepliesById(id);
        }

        public async Task UpdateCommentReplies(CommentRepliesDTO model)
        {
            var existingAddress = await _commentRepliesRepository.GetCommentRepliesById(model.Id);
            if (existingAddress == null)
            {
                throw new KeyNotFoundException($"Address với Id {model.Id} không tồn tại.");
            }
            existingAddress.Content = model.Content;
            existingAddress.UpdatedAt = DateTime.Now;
            existingAddress.Image = model.ImageUrl;

            await _commentRepliesRepository.UpdateCommentReplies(existingAddress);
        }

        public async Task DeleteCommentReplies(int id)
        {
            await _commentRepliesRepository.DeleteCommentReplies(id);
        }

        public async Task<int> AddCommentReplies(CommentRepliesDTO model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Comment reply data is required.");

            if (string.IsNullOrWhiteSpace(model.Content))
                throw new ArgumentException("Reply content cannot be empty.", nameof(model.Content));

            var commentReply = new CommentReplies
            {
                CommentId = model.commentId,
                UserId = model.userId,
                Content = model.Content,
                Image = model.ImageUrl,
                UpdatedAt = DateTime.UtcNow
            };

            var newId = await _commentRepliesRepository.AddNewCommentReplies(commentReply);

            return newId;
        }

    }
}

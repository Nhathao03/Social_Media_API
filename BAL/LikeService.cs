using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class LikeService : ILikeService
    {
        private readonly LikeRepository _likeRepository;

        public LikeService(LikeRepository repository)
        {
            _likeRepository = repository;
        }

        public async Task<IEnumerable<Like>> GetAllLikeAsync()
        {
            return await _likeRepository.GetAllLikes();
        }

        public async Task<Like> GetLikeByIdAsync(int id)
        {
            return await _likeRepository.GetLikeById(id);
        }

        public async Task AddLikeAsync(LikeDTO likeDTO)
        {
            var likeData = new Like
            {
                UserId = likeDTO.UserID,
                PostId = likeDTO.postID,
                CreatedAt = DateTime.UtcNow,
            };

            await _likeRepository.AddLike(likeData);
        }

        public async Task UpdateLikeAsync(Like like)
        {
            await _likeRepository.UpdateLike(like);
        }

        public async Task DeleteLikeAsync(int id)
        {
            await _likeRepository.DeleteLike(id);
        } 

        public async Task<Like?> CheckLikeUserOnPost(string userId, int postId)
        {
           var data = await _likeRepository.GetLikeByUserAndPost(userId, postId);
            return data;
        }

        public async Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId)
        {
            return await _likeRepository.GetLikesByPostId(postId);
        }
    }
}

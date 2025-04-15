using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface ILikeRepository
    {
        Task<IEnumerable<Like>> GetAllLikes();
        Task<Like> GetLikeById(int id);
        Task AddLike(Like like);
        Task UpdateLike(Like like);
        Task DeleteLike(int id);
        Task<Like?> GetLikeByUserAndPost(string userId, int postId);
    }
}

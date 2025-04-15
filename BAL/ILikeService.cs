using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface ILikeService
    {
        Task<IEnumerable<Like>> GetAllLikeAsync();
        Task<Like> GetLikeByIdAsync(int id);
        Task AddLikeAsync(LikeDTO like);
        Task UpdateLikeAsync(Like like);
        Task DeleteLikeAsync(int id);
        Task<Like?> GetLikeByUserAndPostAsync(string userId, int postId);

    }
}

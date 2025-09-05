using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface IPostImageService
    {
        Task<IEnumerable<PostImage>> GetAllPostImageAsync();
        Task<PostImage> GetPostImageByIdAsync(int id);
        Task AddPostImageAsync(PostDTO postDTO, int PostId);
        Task UpdatePostImageAsync(PostImage postimage);
        Task DeletePostImageAsync(int id);
        Task<List<PostImage>> GetAllPostImagesByUserIDAsync(string userId);
        Task<List<PostImage>> GetPostImagesByPostIDAsync(int postId);
    }
}

using Social_Media.Models;

namespace Social_Media.BAL
{
    public interface IPostImageService
    {
        Task<IEnumerable<PostImage>> GetAllPostImageAsync();
        Task<PostImage> GetPostImageByIdAsync(int id);
        Task AddPostImageAsync(PostImage postimage);
        Task UpdatePostImageAsync(PostImage postimage);
        Task DeletePostImageAsync(int id);
        Task<List<PostImage>> GetAllPostImagesByUserIDAsync(string userId);
        Task<List<PostImage>> GetPostImagesByPostIDAsync(int postId);
    }
}

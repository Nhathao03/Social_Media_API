using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface IPostImageRepository
    {
        Task<IEnumerable<PostImage>> GetAllPostImage();
        Task<PostImage> GetPostImageById(int id);
        Task AddPostImage(PostImage post);
        Task UpdatePostImage(PostImage post);
        Task DeletePostImage(int id);
        Task<List<PostImage>> GetAllPostImagesByUserID(string userId);
        Task<List<PostImage>> GetPostImagesByPostID(int postId);
    }
}

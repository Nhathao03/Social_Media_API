using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPost();
        Task<Post> GetPostById(int id);
        Task AddPost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int id);
        Task<IEnumerable<Post>> GetPostsByUserID(string userID);
        Task<IEnumerable<Post>> GetAllPostNearestCreatedAt();
    }
}

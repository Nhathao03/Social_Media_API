using Social_Media.Models;

namespace Social_Media.BAL
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
        Task<IEnumerable<Post>> GetPostsByUserIDAsync(string userID);
        Task<IEnumerable<Post>> GetAllPostNearestCreatedAtAsync();
    }
}

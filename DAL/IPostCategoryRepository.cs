using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface IPostCategoryRepository
    {
        Task<IEnumerable<PostCategory>> GetAllPostCategory();
        Task<PostCategory> GetPostCategoryById(int id);
        Task AddPostCategory(PostCategory post);
        Task UpdatePostCategory(PostCategory post);
        Task DeletePostCategory(int id);
    }
}

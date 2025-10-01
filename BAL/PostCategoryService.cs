using Social_Media.DAL;
using Social_Media.Models;

namespace Social_Media.BAL
{
    public class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryRepository _repository;

        public PostCategoryService(IPostCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PostCategory>> GetAllPostCategory()
        {
            return await _repository.GetAllPostCategory();
        }

        public async Task<PostCategory> GetPostCategoryById(int id)
        {
            return await _repository.GetPostCategoryById(id);
        }

        public async Task AddPostCategory(PostCategory PostCategory)
        {
            await _repository.AddPostCategory(PostCategory);
        }

        public async Task UpdatePostCategory(PostCategory PostCategory)
        {
            await _repository.UpdatePostCategory(PostCategory);
        }

        public async Task DeletePostCategory(int id)
        {
            await _repository.DeletePostCategory(id);
        }
    }
}

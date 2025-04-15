using Social_Media.DAL;
using Social_Media.Models;

namespace Social_Media.BAL
{
    public class PostImageService : IPostImageService
    {
        private readonly PostImageRepository _repository;

        public PostImageService(PostImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PostImage>> GetAllPostImageAsync()
        {
            return await _repository.GetAllPostImage();
        }

        public async Task<PostImage> GetPostImageByIdAsync(int id)
        {
            return await _repository.GetPostImageById(id);
        }

        public async Task AddPostImageAsync(PostImage postimage)
        {
            await _repository.AddPostImage(postimage);
        }

        public async Task UpdatePostImageAsync(PostImage postimage)
        {
            await _repository.UpdatePostImage(postimage);
        }

        public async Task DeletePostImageAsync(int id)
        {
            await _repository.DeletePostImage(id);
        }

        public async Task<List<PostImage>> GetAllPostImagesByUserIDAsync(string userId)
        {
            return await _repository.GetAllPostImagesByUserID(userId);
        }
    }
}

using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

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

        public async Task AddPostImageAsync(PostDTO postDTO, int PostId)
        {

           if (postDTO.PostImages == null || !postDTO.PostImages.Any()) return;

           var postImages = postDTO.PostImages.Select(imageUrl => new PostImage
           {
               PostId = PostId,
               Url = imageUrl.Url,
           }).ToList();

            await _repository.AddPostImage(postImages);
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

        public async Task<List<PostImage>> GetPostImagesByPostIDAsync(int postId)
        {
            return await _repository.GetPostImagesByPostID(postId);
        }
    }
}

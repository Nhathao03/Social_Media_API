using Social_Media.DAL;
using Social_Media.Models;

namespace Social_Media.BAL
{
    public class PostService : IPostService
    {
        private readonly PostRepository _repository;

        public PostService(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _repository.GetAllPost();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _repository.GetPostById(id);
        }

        public async Task AddPostAsync(Post post)
        {
            await _repository.AddPost(post);
        }

        public async Task UpdatePostAsync(Post post)
        {
            await _repository.UpdatePost(post);
        }

        public async Task DeletePostAsync(int id)
        {
            await _repository.DeletePost(id);
        }

        //get Posts by userID
        public async Task<IEnumerable<Post>> GetPostsByUserIDAsync(string userID)
        {
           return await _repository.GetPostsByUserID(userID);
        }

        public async Task<IEnumerable<Post>> GetAllPostNearestCreatedAtAsync()
        {
            return await _repository.GetAllPostNearestCreatedAt();
        }
    }
}

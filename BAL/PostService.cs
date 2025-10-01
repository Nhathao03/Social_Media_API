using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository repository
            , ILogger<PostService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _repository.GetAllPost();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _repository.GetPostById(id);
        }

        public async Task<Post> AddPostAsync(PostDTO postDTO)
        {
            try
            {
                var post = new Post
                {
                    UserID = postDTO.UserId,
                    Content = postDTO.Content,
                    Views = postDTO.ViewsCount ?? 0,
                    Share = postDTO.SharesCount ?? 0,
                    PostCategoryID = postDTO.PostCategoryID,
                    CreatedAt = postDTO.CreatedAt,
                    UpdatedAt = postDTO.UpdatedAt
                };

                var createdPost = await _repository.AddPost(post);

                _logger.LogInformation("Post added successfully with ID: {PostId}", createdPost.ID);

                return createdPost;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new post");
                throw;
            }
            
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
        public async Task<IEnumerable<PostDTO>> GetPostsByUserIDAsync(string userID)
        {
           return await _repository.GetPostsByUserID(userID);
        }

        public async Task<IEnumerable<PostDTO>> GetRecentPostsAsync(int page, int pageSize)
        {
            return await _repository.GetRecentPostsAsync(page, pageSize);
        }
    }
}

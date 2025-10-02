using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities.DTO;
using SocialMedia.Core.Entities.PostEntity;


namespace Social_Media.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostImageService _postImageService;

        public PostController(IPostService postService,
            IPostImageService postImageService)
        {
            _postService = postService;
            _postImageService = postImageService;
            
        }

        // Create a new post
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createPost = await _postService.AddPostAsync(postDto);
            await _postImageService.AddPostImageAsync(postDto, createPost.ID);

            return CreatedAtAction(nameof(GetAllPost), new { id = createPost.ID }, createPost);
        }

        //Get all post
        [HttpGet("getAllPost")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetAllPost()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts == null) return NotFound();
            return Ok(posts);
        }

        //Get post by ID
        [HttpGet("getPostByID/{id}")] 
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        //Update post by ID
        [HttpPut("updatePost/{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (post == null || post.ID != id) return BadRequest();
            await _postService.UpdatePostAsync(post);
            return NoContent();
        }

        //Delete post by ID
        [HttpDelete("deletePostByID/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }

        //Get post by user ID
        [HttpGet("getPostsByUserID/{userID}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetPostsByuserID(string userID)
        {
            var posts = await _postService.GetPostsByUserIDAsync(userID);
            if (posts == null) return NotFound();
            return Ok(posts);
        }

        // Get all post nearest created at
        [HttpGet("GetRecentPostCreated")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetRecentPostCreated()
        {
            var result = await _postService.GetRecentPostsAsync(1,10);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}

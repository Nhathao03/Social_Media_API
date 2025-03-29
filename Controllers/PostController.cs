using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;


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

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO postDto)
        {
            if (postDto == null) return BadRequest();
            var post = new Post
            {
                UserID = postDto.UserID,
                Content = postDto.Content,
                Views = postDto.Views ?? 0,
                Share = postDto.Share ?? 0,
                PostCategoryID = postDto.PostCategoryID,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _postService.AddPostAsync(post);

            if(postDto.PostImages[0] != null)
            {
                for (var i = 0; i < postDto.PostImages.Count; i++)
                {
                    var images = new PostImage
                    {
                        Url = postDto.PostImages[i].Url,
                        PostId = post.ID,
                    };
                    await _postImageService.AddPostImageAsync(images);
                }
            }
            return CreatedAtAction(nameof(GetAllPost), new { id = post.ID }, postDto);
        }

        [HttpGet("getAllPost")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetAllPost()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts == null) return NotFound();
            return Ok(posts);
        }

        [HttpGet("getPostByID/{id}")] 
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }    

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (post == null || post.ID != id) return BadRequest();
            await _postService.UpdatePostAsync(post);
            return NoContent();
        }

        [HttpDelete("deletePostByID/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }

        [HttpGet("getPostsByUserID/{userID}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetPostsByuserID(string userID)
        {
            var posts = await _postService.GetPostsByUserIDAsync(userID);
            if (posts == null) return NotFound();
            return Ok(posts);
        }

        [HttpGet("GetAllPostNearestCreatedAt")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetAllPostNearestCreatedAt()
        {
            var result = await _postService.GetAllPostNearestCreatedAtAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}

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

        public PostController(IPostService postService)
        {
            _postService = postService;
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
                ImageUrl = postDto.ImageUrl,
                PostCategoryID = 6,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _postService.AddPostAsync(post);
            return CreatedAtAction(nameof(GetAllPost), new { id = post.ID }, postDto);
        }

        [HttpGet("getAllPost")]
        public async Task<IActionResult> GetAllPost()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts == null) return NotFound();
            return Ok(posts);
        }

        [HttpGet("{id}")] 
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}

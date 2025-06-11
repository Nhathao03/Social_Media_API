using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;
using Social_Media.Models.DTO;
using Social_Media.Models;

namespace Social_Media.Controllers
{
    [Route("api/postImage")]
    [ApiController]
    public class postImageController : ControllerBase
    {
        private readonly IPostImageService _postImageService;

        public postImageController(IPostImageService postImageService)
        {
            _postImageService = postImageService;
        }

        //Get all post images
        [HttpGet("GetAllPostImage")]
        public async Task<IActionResult> GetAllPostImage()
        {
            var getPostImage = await _postImageService.GetAllPostImageAsync();
            if (getPostImage == null) return NotFound();
            return Ok(getPostImage);
        }

        //Get post image by ID
        [HttpGet("GetPostImagesByUserID/{id}")]
        public async Task<IActionResult> GetPostImagesByUserID(string id)
        {
            var getPostImage = await _postImageService.GetAllPostImagesByUserIDAsync(id);
            if (getPostImage == null) return NotFound();
            return Ok(getPostImage);
        }


        //CHECK AND REVISE THIS CODE AGAIN
        //Upload post image
       
        public async Task<IActionResult> CreatePost([FromBody] PostDTO postDto)
        {
            if (postDto == null || postDto.UserID == null) return BadRequest();
           
            if (postDto.PostImages[0] != null)
            {
                for (var i = 0; i < postDto.PostImages.Count; i++)
                {
                    var images = new PostImage
                    {
                        Url = postDto.PostImages[i].Url,
                        //PostId = post.ID,
                    };
                    await _postImageService.AddPostImageAsync(images);
                }
            }
            return Ok("Upload post image susccesss");
        }

        //Get pos image by post ID
        [HttpGet("GetPostImageByPostID/{postId}")]
        public async Task<IActionResult> GetPostImageByPostID(int postId)
        {
            var postImage = await _postImageService.GetPostImagesByPostIDAsync(postId);
            if (postImage == null) return NotFound();
            return Ok(postImage);
        }
    }
}

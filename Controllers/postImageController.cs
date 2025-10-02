using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities.DTO;
using SocialMedia.Core.Entities;

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

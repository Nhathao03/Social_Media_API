using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;

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

        [HttpGet("GetAllPostImage")]
        public async Task<IActionResult> GetAllPostImage()
        {
            var getPostImage = await _postImageService.GetAllPostImageAsync();
            if (getPostImage == null) return NotFound();
            return Ok(getPostImage);
        }

        [HttpGet("GetPostImagesByUserID/{id}")]
        public async Task<IActionResult> GetPostImagesByUserID(string id)
        {
            var getPostImage = await _postImageService.GetAllPostImagesByUserIDAsync(id);
            if (getPostImage == null) return NotFound();
            return Ok(getPostImage);
        }
    }
}

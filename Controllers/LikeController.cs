using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.Controllers
{
    [Route("api/like")]
    [ApiController]
    public class LikeController : ControllerBase
    { 
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("AddLike")]
        public async Task<IActionResult> AddLike([FromBody] LikeDTO likeDTO)
        {
            if (likeDTO == null ) return BadRequest();

            // Check userID and postID
            var existingLike = await _likeService.GetLikeByUserAndPostAsync(likeDTO.UserID, likeDTO.postID);
            if (existingLike != null)
            {
                deleteLikeByID(existingLike.ID);
                return BadRequest("Deleted like this post.");
            }
            await _likeService.AddLikeAsync(likeDTO);
            return Ok("Add new like post sucess !");
        }

        [HttpGet("GetAllLike")]
        public async Task<IActionResult> getAllLike()
        {
            var like = await _likeService.GetAllLikeAsync();
            if (like == null) return NotFound();
            return Ok(like);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getLikeByID (int id)
        {
            var like  = _likeService.GetLikeByIdAsync(id);
            if(like == null) return NotFound();
            return Ok(like);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteLikeByID(int id)
        {
            await _likeService.DeleteLikeAsync(id);
            return NoContent();
        }
    }
}

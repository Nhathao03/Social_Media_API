using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Entities.DTO;

namespace Social_Media.Controllers
{
    [Route("api/like")]
    [ApiController]
    public class LikeController : ControllerBase
    { 
        private readonly ILikeService _likeService;
        private readonly ICommentService _commentService;

        public LikeController(ILikeService likeService,
            ICommentService commentService)
        {
            _likeService = likeService;
            _commentService = commentService;
        }

        //Add like to post
        [HttpPost("AddLike")]
        public async Task<IActionResult> AddLike([FromBody] LikeDTO likeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check userID and postID
            var existingLike = await _likeService.CheckLikeUserOnPost(likeDTO.UserID, likeDTO.postID);
            if (existingLike != null)
            {
                deleteLikeByID(existingLike.ID);
                return BadRequest("Deleted like this post.");
            }
            await _likeService.AddLikeAsync(likeDTO);
            return Ok("Add new like post sucess !");
        }

        //Get all likes
        [HttpGet("GetAllLike")]
        public async Task<IActionResult> getAllLike()
        {
            var like = await _likeService.GetAllLikeAsync();
            if (like == null) return NotFound();
            return Ok(like);
        }

        //Get like by ID
        [HttpGet("getLikebyId/{id}")]
        public async Task<IActionResult> getLikeByID (int id)
        {
            var like  = _likeService.GetLikeByIdAsync(id);
            if(like == null) return NotFound();
            return Ok(like);
        }

        //Delete like by ID
        [HttpDelete("deleteLikeByID/{id}")]
        public async Task<IActionResult> deleteLikeByID(int id)
        {
            await _likeService.DeleteLikeAsync(id);
            return NoContent();
        }

        //Add like to comment by ID
        [HttpPost("AddLikeComment/{id}")]
        public async Task<IActionResult> AddLikeComment(int id)
        {
            var commentData = await _commentService.GetCommentByIdAsync(id);
            if (commentData == null) return NotFound("Comment not found.");

            //await _commentService.UpdateCommentAsync(dataUpdate);

            return NoContent();
        }

        // add like to comment by comment ID and user ID

        //Get all likes by post ID
        [HttpGet("GetLikesByPostID/{postID}")]
        public async Task<IActionResult> GetLikesByPostID(int postID)
        {
            var likes = await _likeService.GetLikesByPostIdAsync(postID);
            if (likes == null) return NotFound();
            return Ok(likes);
        }
    }
}

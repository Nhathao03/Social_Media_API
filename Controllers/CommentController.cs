using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.Controllers
{

    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //Add new comment on post
        [HttpPost("CreateNewComment")]
        public async Task<IActionResult> CreateNewComment([FromBody] CommentDTO commentDTO)
        {
            if (commentDTO == null) return BadRequest();
            await _commentService.AddCommentAsync(commentDTO);
            return Ok("Create new comment success !");
        }

        //Get all comments
        [HttpGet("GetAllComment")]
        public async Task<IActionResult> getAllComment()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            if (comments == null) return NotFound();
            return Ok(comments);
        }

        //Get comment by ID
        [HttpGet("getCommentByID/{id}")]
        public async Task<IActionResult> getCommentByID(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }


        //Delete comment by ID
        [HttpDelete("deleteCommentByID/{id}")]
        public async Task<IActionResult> deleteCommentByID (int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }

        //Add like to comment by ID
        [HttpPut("AddLikeComment/{id}")]
        public async Task<IActionResult> AddLikeComment(int id)
        {
            var commentData = await _commentService.GetCommentByIdAsync(id);
            if (commentData == null) return NotFound("Comment not found.");

            commentData.Sticker += 1; 

            await _commentService.UpdateCommentAsync(commentData);

            return NoContent();
        }

        //Add comment by post ID
        [HttpGet("getCommentByPostID/{postID}")]
        public async Task<IActionResult> getCommentByPostID(int postID)
        {
            var comment = await _commentService.GetCommentByPostIDAsync(postID);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        //Update comment by ID
        [HttpPut("updateComment")]
        public async Task<IActionResult> updateComment([FromBody] Comment comment)
        {
            if (comment == null) return BadRequest();
            await _commentService.UpdateCommentAsync(comment);
            return Ok("Update comment success");
        }

    }
}

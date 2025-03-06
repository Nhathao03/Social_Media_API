﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        [HttpPost("CreateNewComment")]
        public async Task<IActionResult> CreateNewComment([FromBody] CommentDTO commentDTO)
        {
            if (commentDTO == null) return BadRequest();
            var comment = new Comment
            {
                UserId = commentDTO.userID,
                PostId = commentDTO.postID,
                Sticker = commentDTO.sticker,
                Content = commentDTO.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Image = commentDTO.ImageUrl ?? null,
            };

            await _commentService.AddCommentAsync(comment);
            return Ok("Create new comment success !");
        }

        [HttpGet("GetAllComment")]
        public async Task<IActionResult> getAllComment()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            if (comments == null) return NotFound();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getCommentByID(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCommentByID (int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}

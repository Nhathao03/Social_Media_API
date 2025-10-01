using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models.DTO.Comment
{
    public class CommentRepliesDTO
    {
        public int Id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public int commentId { get; set; }
        [Required]
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

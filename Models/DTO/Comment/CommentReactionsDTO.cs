using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models.DTO.Comment
{
    public class CommentReactionsDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int CommentId { get; set; }
        [Required]
        public string ReactionType { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

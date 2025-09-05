using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models.DTO
{
    public class PostDTO
    {
        [Required(ErrorMessage ="UserId is required")]
        public string UserId { get; set; }
        public string? Content { get; set; }
        public int? ViewsCount { get; set; }
        public int? SharesCount { get; set; }
        public List<PostImageDTO>? PostImages { get; set; }
        [Required]
        public int PostCategoryID { get; set; }
        public int? postId { get; set; }
        public string? Username { get; set; }
        public string? UserAvatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public PostCategoryDTO? postCategory { get; set; }
        public List<CommentDTO>? Comments { get; set; }
        public List<LikeDTO>? Likes { get; set; }
    }

    public class PostCategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}

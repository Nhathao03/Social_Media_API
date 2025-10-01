using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Post
	{
		[Key]
		public int ID { get; set; }
		public string? UserID { get; set; }
		public string? Content { get; set; }
		public int? Views { get; set; }
        public int? Share { get; set; }
        public List<PostImage>? PostImages { get; set; }
        public int PostCategoryID { get; set; }
		public PostCategory? PostCategory { get; set; } 
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User user { get; set; }

        // Navigation properties
        public ICollection<Comment>? Comments { get; set; }
		public ICollection<Like>? Likes { get; set; }
		public ICollection<Report>? Reports { get; set; }
    }
}

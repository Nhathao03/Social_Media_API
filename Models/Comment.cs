using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
	public class Comment
	{
		[Key]
		public int ID { get; set; }	
		public string? Image {  get; set; }
		public int PostId { get; set; }
		public string UserId { get; set; }
		public string? Content { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; 

		// Navigation properties
		public Post Post { get; set; }
        public User user { get; set; }
		public ICollection<CommentReplies>? CommentReplies { get; set; }
		public ICollection<CommentReactions> CommentReactions { get; set; }
    }
}

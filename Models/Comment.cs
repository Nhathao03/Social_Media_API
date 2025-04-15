using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
	public class Comment
	{
		[Key]
		public int ID { get; set; }	
		public int Sticker { get; set; }
		public string? Reply { get; set; }
		public string? Image {  get; set; }
		public int PostId { get; set; }
		public string UserId { get; set; }
		public string? Content { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		public Post Post { get; set; }
	}
}

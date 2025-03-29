using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Message
	{
		[Key]
		public int ID { get; set; }
		public string SenderId { get; set; }
		public string ReceiverId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	}
}

using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Message
	{
		[Key]
		public int ID { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	}
}

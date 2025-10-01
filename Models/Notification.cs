using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationship
        public User User { get; set; }
    }

}

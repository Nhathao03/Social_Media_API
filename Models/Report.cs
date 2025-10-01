using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationship
        public Post? Post { get; set; }
        public User? User { get; set; }
    }

}

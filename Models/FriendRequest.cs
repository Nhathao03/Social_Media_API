using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class FriendRequest
    {
        [Key]
        public int ID { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public int status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

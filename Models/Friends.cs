using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Friends
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public string FriendID { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public int Type_FriendsID { get; set; }
        public Type_Friends? Type_Friends { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Follower
    {
        [Key]
        public int Id { get; set; }
        public string userID { get; set; }
        public string userFollowerID { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Following
    {
        [Key]
        public int ID { get; set; }
        public string userID { get; set; }
        public string pageFollowerID { get; set; }
        public string userFollowerID { get; set; }
    }
}

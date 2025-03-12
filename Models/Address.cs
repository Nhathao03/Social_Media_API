using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public string name_with_type { get; set; }
        public List<User>? users { get; set; }
    }
}

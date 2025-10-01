using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class RoleCheck
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; } = null!;
        public string RoleID { get; set; }

        // Relationship
        public Role? Role { get; set; }
        public User? User { get; set; }
    }
}

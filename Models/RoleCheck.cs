using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class RoleCheck
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }
        public string RoleID { get; set; }
    }
}

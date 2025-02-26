using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    
    }
}

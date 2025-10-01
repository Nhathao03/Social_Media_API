using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class Role
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; } = null!;

        // Relationship
        public ICollection<RoleCheck>? RoleChecks { get; set; }
    }
}

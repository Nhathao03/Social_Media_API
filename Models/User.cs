using System;
using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime? Birth { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public string? BackgroundProfile { get; set; }
        public string? Avatar { get; set; }
        public List<Follower> follower { get; set; }
        public List<Following> following { get; set; }
        public string? gender { get; set; }
        public int? addressID { get; set; }
        public string NormalizeUsername { get; set; }
        public string NormalizeEmail { get; set; }
        public Address? address { get; set; }

        public ICollection<Post>? posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Report>? Reports { get; set; }
        public ICollection<RoleCheck>? RoleChecks { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must contain '@' and end with a domain.")]
        public string Email { get; set; }

        public DateTime? Birth { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

        public string? BackgroundProfile { get; set; }
        public string? Avatar { get; set; }
        public List<Follower> follower { get; set; }
        public List<Following> following { get; set; }
        public string? gender { get; set; }
        public int? addressID { get; set; }
        public Address? address { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models.DTO
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; } 
        [Required(ErrorMessage = "Message is required")]
        [MaxLength(250, ErrorMessage = "Message cannot exceed 250 characters")]
        [MinLength(5, ErrorMessage = "Message must be at least 5 characters long")]
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

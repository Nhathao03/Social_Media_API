using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models.DTO
{
    public class ReportDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "PostId is required")]
        public int PostId { get; set; }
        [Required(ErrorMessage = "Reason is required")]
        [MaxLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        [MinLength(10, ErrorMessage = "Reason must be at least 10 characters long")]
        public string Reason { get; set; }
        public DateTime ReportedAt { get; set; }
        public int Status { get; set; }
    }
}

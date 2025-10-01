namespace Social_Media.Models.DTO.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string userID { get; set; }
        public int postID { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserName_comment { get; set; }
        public string? UserAvatar_comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

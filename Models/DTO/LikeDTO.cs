namespace Social_Media.Models.DTO
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int postID { get; set; }
        public string? UserName_like { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

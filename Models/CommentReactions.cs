using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Social_Media.Models
{
    public class CommentReactions
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CommentId { get; set; }
        public string ReactionType { get; set; } // e.g., "like", "love", "haha", etc.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Comment Comment { get; set; }
    }
}

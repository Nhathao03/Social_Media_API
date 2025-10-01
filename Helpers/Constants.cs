using System.ComponentModel;

namespace Social_Media.Helpers
{
    public class Constants
    {
        public enum FriendRequestEnum
        {
            [Description("Pending")]
            Pending = 1,
            [Description("Accepted")]
            Accepted = 2,
        }

        public enum FriendsEnum
        {
            [Description("Close friends")]
            Close_Friends = 1,
            [Description("Home Town")]
            Home_Town = 2,
            [Description("normal")]
            normal = 3,

        }

        public enum NotificationTypeEnum
        {
            [Description("Like")]
            Like = 1,
            [Description("Comment")]
            Comment = 2,
            [Description("FriendRequest")]
            FriendRequest = 3,
            [Description("PostShare")]
            PostShare = 4,
            [Description("Message")]
            Message = 5,
        }

        public enum ReportStatusEnum
        {
            [Description("Pending")]
            Pending = 1,
            [Description("Resolved")]
            Resolved = 2,
        }
    }
}

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
            [Description("Rejected")]
            Rejected = 3,
            [Description("Cancelled")]
            Cancelled = 4
        }
    }
}

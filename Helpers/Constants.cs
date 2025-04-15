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
            normal = 5,

        }
    }
}

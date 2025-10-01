using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface IFriendsService
    {
        Task<IEnumerable<Friends>> GetAllFriendsAsync();
        Task<Friends> GetFriendsByIdAsync(int id);
        Task AddFriendsAsync(int id);
        Task UpdateFriendsAsync(FriendDTO Friends);
        Task DeleteFriendsAsync(int id);
        Task<IEnumerable<Friends>> GetFriendsByUserIDAsync(string userID);
        Task<IEnumerable<Friends>> getFriendRecentlyAdded(string  userID);
        Task<IEnumerable<string>> getFriendOfEachUser(string userId);
        Task<IEnumerable<Friends>> GetFriendBaseOnHomeTown(string userId);
    }
}

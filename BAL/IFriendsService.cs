using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface IFriendsService
    {
        Task<IEnumerable<Friends>> GetAllFriendsAsync();
        Task<Friends> GetFriendsByIdAsync(int id);
        Task AddFriendsAsync(int id);
        Task UpdateFriendsAsync(Friends Friends);
        Task DeleteFriendsAsync(int id);
        Task<List<Friends>> GetFriendsByUserIDAsync(string userID);
        Task<List<Friends>> getFriendRecentlyAdded(string  userID);
        Task<List<string>> getFriendOfEachUser(string userId);
        Task<List<Friends>> GetFriendBaseOnHomeTown(string userId);
    }
}

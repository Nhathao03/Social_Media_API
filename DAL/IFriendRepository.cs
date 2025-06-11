using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface IFriendRepository
    {
        Task<IEnumerable<Friends>> GetAllFriends();
        Task<Friends> GetFriendById(int id);
        Task AddFriend(Friends friends);
        Task UpdateFriend(Friends friends);
        Task DeleteFriend(int id);
        Task<List<Friends>> GetFriendsByUserID(string userId);
        Task<List<Friends>> getFriendRecentlyAdded(string userId);
        Task<List<string>> GetFriendOfEachUser(string userId);
        Task<List<Friends>> GetFriendBaseOnHomeTown(string userId);    
    }
}

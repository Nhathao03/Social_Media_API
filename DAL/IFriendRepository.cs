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
        Task<IEnumerable<Friends>> GetFriendsByUserID(string userId);
        Task<IEnumerable<Friends>> getFriendRecentlyAdded(string userId);
        Task<IEnumerable<string>> GetFriendOfEachUser(string userId);
        Task<IEnumerable<Friends>> GetFriendBaseOnHomeTown(string userId);    
    }
}

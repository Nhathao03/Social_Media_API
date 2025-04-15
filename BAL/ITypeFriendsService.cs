using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface ITypeFriendsService
    {
        Task<IEnumerable<Type_Friends>> GetAllTypeFriendsAsync();
        Task<Type_Friends> GetTypeFriendsByIdAsync(int id);
        Task AddTypeFriendsAsync(TypeFriendsDTO TypeFriends);
        Task UpdateTypeFriendsAsync(Type_Friends TypeFriends);
        Task DeleteTypeFriendsAsync(int id);
    }
}

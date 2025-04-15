using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface ITypeFriendsRepository
    {
        Task<IEnumerable<Type_Friends>> GetAllTypeFriends();
        Task<Type_Friends> GetTypeFriendsById(int id);
        Task AddTypeFriends(TypeFriendsDTO TypeFriends);
        Task UpdateTypeFriends(Type_Friends TypeFriends);
        Task DeleteTypeFriends(int id);
    }
}

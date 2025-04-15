using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class TypeFriendsService : ITypeFriendsService
    {
        private readonly TypeFriendsRepository _TypeFriendsRepository;

        public TypeFriendsService(TypeFriendsRepository repository)
        {
            _TypeFriendsRepository = repository;
        }

        public async Task<IEnumerable<Type_Friends>> GetAllTypeFriendsAsync()
        {
            return await _TypeFriendsRepository.GetAllTypeFriends();
        }

        public async Task<Type_Friends> GetTypeFriendsByIdAsync(int id)
        {
            return await _TypeFriendsRepository.GetTypeFriendsById(id);
        }

        public async Task AddTypeFriendsAsync(TypeFriendsDTO TypeFriends)
        {
            await _TypeFriendsRepository.AddTypeFriends(TypeFriends);
        }

        public async Task UpdateTypeFriendsAsync(Type_Friends TypeFriends)
        {
            await _TypeFriendsRepository.UpdateTypeFriends(TypeFriends);
        }

        public async Task DeleteTypeFriendsAsync(int id)
        {
            await _TypeFriendsRepository.DeleteTypeFriends(id);
        } 
    }
}

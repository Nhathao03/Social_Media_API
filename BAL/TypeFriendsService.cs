using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class TypeFriendsService : ITypeFriendsService
    {
        private readonly ITypeFriendsRepository _TypeFriendsRepository;

        public TypeFriendsService(ITypeFriendsRepository repository)
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
            var data = new Type_Friends
            {
                Type = TypeFriends.Name
            };
            await _TypeFriendsRepository.AddTypeFriends(data);
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

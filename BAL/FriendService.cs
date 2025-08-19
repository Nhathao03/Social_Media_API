using Social_Media.DAL;
using Social_Media.Helpers;
using Social_Media.Models;
using Social_Media.Models.DTO;
using System.Reflection.Metadata;

namespace Social_Media.BAL
{
    public class FriendService : IFriendsService
    {
        private readonly FriendRepository _FriendRepository;
        private readonly FriendRequestRepository _FriendRequestRepository;

        public FriendService(FriendRepository repository,
            FriendRequestRepository friendRequestRepository)
        {
            _FriendRepository = repository;
            _FriendRequestRepository = friendRequestRepository;
        }

        public async Task<IEnumerable<Friends>> GetAllFriendsAsync()
        {
            return await _FriendRepository.GetAllFriends();
        }

        public async Task<Friends> GetFriendsByIdAsync(int id)
        {
            return await _FriendRepository.GetFriendById(id);
        }

        public async Task AddFriendsAsync(int id)
        {
            var getFriendRequest = await _FriendRequestRepository.GetFriendRequestById(id);
            var data = new Friends
            {
                FriendID = getFriendRequest.ReceiverID,
                UserID = getFriendRequest.SenderID,
                Type_FriendsID = (int)Constants.FriendsEnum.normal,
            };

            await _FriendRepository.AddFriend(data);
        }

        public async Task UpdateFriendsAsync(Friends Friends)
        {
            await _FriendRepository.UpdateFriend(Friends);
        }

        public async Task DeleteFriendsAsync(int id)
        {
            await _FriendRepository.DeleteFriend(id);
        }

        public async Task<List<Friends>> GetFriendsByUserIDAsync(string userID)
        {
            return await _FriendRepository.GetFriendsByUserID(userID);
        }

        public async Task<List<Friends>> getFriendRecentlyAdded(string userID)
        {
           return await _FriendRepository.getFriendRecentlyAdded(userID);
        }

        public async Task<List<string>> getFriendOfEachUser(string userId)
        {
            return await _FriendRepository.GetFriendOfEachUser(userId);
        }

        public async Task<List<Friends>> GetFriendBaseOnHomeTown(string userId)
        {
            return await _FriendRepository.GetFriendBaseOnHomeTown(userId);
        }
    }
}

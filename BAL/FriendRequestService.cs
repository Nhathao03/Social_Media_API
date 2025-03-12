using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly FriendRequestRepository _FriendRequestRepository;

        public FriendRequestService(FriendRequestRepository repository)
        {
            _FriendRequestRepository = repository;
        }

        public async Task<IEnumerable<FriendRequest>> GetAllFriendRequestAsync()
        {
            return await _FriendRequestRepository.GetAllFriendRequests();
        }

        public async Task<FriendRequest> GetFriendRequestByIdAsync(int id)
        {
            return await _FriendRequestRepository.GetFriendRequestById(id);
        }

        public async Task AddFriendRequestAsync(FriendRequestDTO FriendRequest)
        {
            await _FriendRequestRepository.AddFriendRequest(FriendRequest);
        }

        public async Task UpdateFriendRequestAsync(FriendRequest FriendRequest)
        {
            await _FriendRequestRepository.UpdateFriendRequest(FriendRequest);
        }

        public async Task DeleteFriendRequestAsync(int id)
        {
            await _FriendRequestRepository.DeleteFriendRequest(id);
        } 
    }
}

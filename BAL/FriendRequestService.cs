using Microsoft.EntityFrameworkCore;
using Social_Media.DAL;
using Social_Media.Helpers;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestRepository _FriendRequestRepository;

        public FriendRequestService(IFriendRequestRepository repository)
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

        public async Task AddFriendRequestAsync(FriendRequestDTO friendRequestDTO)
        {
             var request = new FriendRequest
            {
                SenderID = friendRequestDTO.SenderID,
                ReceiverID = friendRequestDTO.ReceiverID,
                status = (int)Constants.FriendRequestEnum.Pending,
            };
            await _FriendRequestRepository.AddFriendRequest(request);
        }

        public async Task UpdateFriendRequestAsync(FriendRequestDTO modelDTO)
        {
            var FriendRequest = await _FriendRequestRepository.GetFriendRequestById(modelDTO.Id);
            if (FriendRequest == null)
            {
                throw new KeyNotFoundException($"Friend request with ID {modelDTO.Id} not found.");
            }
            FriendRequest.SenderID = modelDTO.SenderID;
            FriendRequest.ReceiverID = modelDTO.ReceiverID;
            await _FriendRequestRepository.UpdateFriendRequest(FriendRequest);
        }

        public async Task DeleteFriendRequestAsync(int id)
        {
            await _FriendRequestRepository.DeleteFriendRequest(id);
        }
        public async Task<IEnumerable<FriendRequest>> GetFriendRequestByReceiverID(string id)
        {
            return await _FriendRequestRepository.GetFriendRequestByReceiverID(id);
        }

        public async Task<IEnumerable<FriendRequest>> GetFriendRequestByUserID(string id)
        {
            return await _FriendRequestRepository.GetFriendRequestByUserID(id);
        }

        //REVISE THIS CODE
        public async Task ConfirmRequest(int id)
        {
            var request = await _FriendRequestRepository.GetFriendRequestById(id);
            if (request == null)
            {
                throw new KeyNotFoundException($"Friend request with ID {id} not found.");
            }
            request.status = (int)Constants.FriendRequestEnum.Accepted;
            await _FriendRequestRepository.UpdateFriendRequest(request);
        }

    }
}

using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface IFriendRequestService
    {
        Task<IEnumerable<FriendRequest>> GetAllFriendRequestAsync();
        Task<FriendRequest> GetFriendRequestByIdAsync(int id);
        Task UpdateFriendRequestAsync(FriendRequest FriendRequest);
        Task DeleteFriendRequestAsync(int id);
        Task AddFriendRequestAsync(FriendRequestDTO FriendRequest);
        Task<List<FriendRequest>> GetFriendRequestBySenderID(string id);
        Task ConfirmRequest(int id);

    }
}

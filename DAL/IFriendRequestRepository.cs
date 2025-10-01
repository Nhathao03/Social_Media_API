using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface IFriendRequestRepository
    {
        Task<IEnumerable<FriendRequest>> GetAllFriendRequests();
        Task<FriendRequest> GetFriendRequestById(int id);
        Task AddFriendRequest(FriendRequest FriendRequest);
        Task UpdateFriendRequest(FriendRequest FriendRequest);
        Task DeleteFriendRequest(int id);
        Task<IEnumerable<FriendRequest>> GetFriendRequestByReceiverID(string id);
        Task<IEnumerable<FriendRequest>> GetFriendRequestByUserID(string id);
    }
}

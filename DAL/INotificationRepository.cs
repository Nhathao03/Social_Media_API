using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface INotificationRepository
    {
        Task CreateNotification(Notification model);
        Task<IEnumerable<Notification>> GetNotificationsByUserId(string userId);
        Task MarkAsRead(int notificationId);
        Task DeleteNotification(int notificationId);
    }
}

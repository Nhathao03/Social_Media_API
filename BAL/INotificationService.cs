using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface INotificationService
    {
        Task<int> GetNotificationCount(string userId);
        Task MarkAllAsRead(string userId);
        Task CreateNotification(NotificationDTO model);
        Task DeleteNotification(int notificationId);

    }
}

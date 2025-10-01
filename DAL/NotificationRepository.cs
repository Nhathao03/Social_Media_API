using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateNotification(Notification model)
        {
            _context.notifications.Add(model);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(string userId)
        {
            return await _context.notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt).ToListAsync();
        }
        public async Task MarkAsRead(int notificationId)
        {
            var notification = await _context.notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteNotification(int notificationId)
        {
            var notification = await _context.notifications.FindAsync(notificationId);
            if (notification != null)
            {
                _context.notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }

    }
}

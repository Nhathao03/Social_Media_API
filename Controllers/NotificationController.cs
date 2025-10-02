using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities.DTO;

namespace Social_Media.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        //Create a new notification
        [HttpPost("CreateNotification")]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDTO model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Message))
            {
                return BadRequest("UserId and message are required.");
            }

            await _notificationService.CreateNotification(model);
            return Ok("Notification created successfully.");
        }

        //Get unread notification count for a user
        [HttpGet("GetNotificationCount/{userId}")]
        public async Task<IActionResult> GetNotificationCount(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is required.");
            }

            var count = await _notificationService.GetNotificationCount(userId);
            return Ok(count);
        }
        //Mark all notifications as read for a user
        [HttpPost("MarkAllAsRead/{userId}")]
        public async Task<IActionResult> MarkAllAsRead(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is required.");
            }

            await _notificationService.MarkAllAsRead(userId);
            return Ok("All notifications marked as read.");
        }

        //Delete a notification by ID
        [HttpDelete("DeleteNotification/{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            await _notificationService.DeleteNotification(notificationId);
            return Ok("Notification deleted successfully.");
        }


    }
}

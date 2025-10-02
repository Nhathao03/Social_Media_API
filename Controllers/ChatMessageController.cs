using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Entities.DTO;
using Social_Media.Hubs;

namespace Social_Media.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatMessageController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        //Get all messages
        [HttpGet("getAllMessages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessageAsync();
            return Ok(messages);
        }

        //Get message by ID
        [HttpGet("getMessageById/{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        //Send message
        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _messageService.AddMessageAsync(messageDTO);
                
                // Send real-time notifications using SignalR
                await _hubContext.Clients.User(messageDTO.ReceiverID).SendAsync("ReceiveMessage", messageDTO.SenderID, messageDTO.Content);
                await _hubContext.Clients.User(messageDTO.SenderID).SendAsync("ReceiveMessage", messageDTO.SenderID, messageDTO.Content);
                
                return Ok(new { message = "Message sent successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Get messages by receiver and sender IDs
        [HttpGet("getMessage/{userId1}/{userId2}")]
        public async Task<IActionResult> GetMessages(string userId1, string userId2)
        {
            try
            {
                var messages = await _messageService.GetMessageByReceiverIdAndSenderIdAsync(userId1, userId2);
                if (messages == null || !messages.Any())
                {
                    return NotFound(new { message = "No messages found" });
                }
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Update message by ID
        [HttpPut("updateMessage/{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] Message message)
        {
            if (id != message.ID)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _messageService.UpdateMessageAsync(message);
                return Ok(new { message = "Message updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Delete message by ID
        [HttpDelete("deleteMessage/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messageService.DeleteMessageAsync(id);
                return Ok(new { message = "Message deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get latest message between two users
        [HttpGet("getLastestMessage/{userId1}/{userId2}")]
        public async Task<IActionResult> GetLatestMessage(string userId1, string userId2)
        {
            try
            {
                var latestMessage = await _messageService.GetMessageLastestAsync(userId1, userId2);
                if (latestMessage == null || !latestMessage.Any())
                {
                    return NotFound(new { message = "No latest message found" });
                }
                return Ok(latestMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

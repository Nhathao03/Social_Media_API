using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessageAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDTO)
        {
            try
            {
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

        [HttpGet("getMessages/{receiverID}/{senderID}")]
        public async Task<IActionResult> GetMessages(string receiverID, string senderID)
        {
            try
            {
                var messages = await _messageService.GetMessageByReceiverIDAsync(receiverID, senderID);
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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] Message message)
        {
            if (id != message.ID)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            try
            {
                await _messageService.UpdateMessageAsync(message);
                return Ok(new { message = "Message updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
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
    }
}

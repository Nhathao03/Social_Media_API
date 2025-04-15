using Microsoft.AspNetCore.SignalR;

namespace Social_Media.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderId, string receiverId, string content)
        {
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content);
            await Clients.User(senderId).SendAsync("ReceiveMessage", senderId, content);
        }
    }
} 
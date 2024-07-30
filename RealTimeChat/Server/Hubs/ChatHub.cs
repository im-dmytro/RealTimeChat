using Microsoft.AspNetCore.SignalR;

namespace RealTimeChat.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string sentiment)
        {
            await Clients.All.SendAsync("RecieveMessage", user, message, sentiment);
        }
    }
}

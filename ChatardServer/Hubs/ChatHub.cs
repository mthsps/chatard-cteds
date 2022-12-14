using Microsoft.AspNetCore.SignalR;

namespace ChatardServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string sender, string receiver)
        {
            await Clients.All.SendAsync("ReceiveMessage", sender, receiver);
        }
    }
}

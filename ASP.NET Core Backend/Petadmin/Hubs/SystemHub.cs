using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Petadmin.Hubs
{
    public class SystemHub : Hub
    {
        public async Task NewMessage(long username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

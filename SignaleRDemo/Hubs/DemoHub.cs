using Microsoft.AspNet.SignalR;

namespace SignaleRDemo.Hubs
{
    
    public class DemoHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.ping(name, message);
        }
    }
}
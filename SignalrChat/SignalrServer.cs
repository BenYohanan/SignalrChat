using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrChat
{
    public class SignalrServer : Hub
    {
        public async Task SendMessage(string userName, string message)
        {
          await  Clients.All.SendAsync("ReceiveMessage", userName, message);
        }
    }
}

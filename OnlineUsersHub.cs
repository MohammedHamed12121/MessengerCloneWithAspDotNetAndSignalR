using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RealTimeChatApp
{
    public class OnlineUsersHub : Hub
    {
       public static int UsersCounter=0;

        public async Task SendUsersCounter()
        {
            //send users online to all users
            await Clients.All.SendAsync("GetUsersCounter",UsersCounter);
        }

        public override async Task OnConnectedAsync()
        {
            //raise when user connect to site
            UsersCounter++;
            SendUsersCounter();
            //
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //raise when user disconnect site
            UsersCounter--;
            SendUsersCounter();
            //
            await base.OnDisconnectedAsync(exception); 
        }
    }
}
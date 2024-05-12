using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Data;

namespace RealTimeChatApp
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string fromId, string toId, string message)
        {

            // get the fromId name 
            var fromUser = await _context.Users.Where(u => u.Id == fromId).FirstOrDefaultAsync();
            var fromName= fromUser.UserName;
            // get the toId name 
            // var toName = await _context.Users.Where(u => u.Id == toId).FirstOrDefaultAsync();

            await Clients.User(toId).SendAsync("ReceiveMessage", fromName, message);
            await Clients.User(fromId).SendAsync("ReceiveMessage", fromName, message);
        }
        
    }
}
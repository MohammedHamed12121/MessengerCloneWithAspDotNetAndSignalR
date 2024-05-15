using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Data;
using RealTimeChatApp.Interfaces;
using RealTimeChatApp.Models;

namespace RealTimeChatApp
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly IMessageRepository _messageRepo;

        public ChatHub(ApplicationDbContext context, IMessageRepository messageRepo)
        {
            _context = context;
            _messageRepo = messageRepo;
        }

        public async Task SendMessage(string fromId, string toId, string message)
        {

            // get the fromId name 
            var fromUser = await _context.Users.Where(u => u.Id == fromId).FirstOrDefaultAsync();
            var fromName= fromUser.UserName;
            // get the toId name 
            // var toName = await _context.Users.Where(u => u.Id == toId).FirstOrDefaultAsync();

            // save message to the context
            var newMessage = new Message(){
                ToUserId = toId,
                FromUserId = fromId,
                Content = message,
                CreatedAt = DateTime.Now
            };
            _messageRepo.Add(newMessage);

            await Clients.User(toId).SendAsync("ReceiveMessage", fromId, toId, message);
            await Clients.User(fromId).SendAsync("ReceiveMessage", fromId, toId, message);
        }
        
    }
}
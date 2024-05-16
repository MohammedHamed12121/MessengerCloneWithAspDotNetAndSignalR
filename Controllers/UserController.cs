using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Interfaces;
using RealTimeChatApp.Models;
using RealTimeChatApp.ViewModels;

namespace RealTimeChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMessageRepository _messageRepo;

        public UserController(UserManager<IdentityUser> userManager, IMessageRepository messageRepo)
        {
            _userManager = userManager;
            _messageRepo = messageRepo;
        }

        public async Task<UsersWithMessagesViewModel> GetAll()
        {
            string currUser = _userManager.GetUserId(User);
            var users = await _userManager.Users.ToListAsync();

            List<Message> messagesobject = new();            
            foreach(IdentityUser user in users)
            {
                var allMessages = await _messageRepo.GetAllAsync(currUser, user.Id);
                if(allMessages.Count != 0)
                {
                    var messageObject = allMessages[^1];
                    messagesobject.Add(messageObject);
                }
            }

            messagesobject = messagesobject.OrderByDescending(m => m.CreatedAt).ToList();

            var usersWithMessagesVM = new UsersWithMessagesViewModel()
            {
                MessagesObject = messagesobject,
                curUserId = currUser
            };

            return usersWithMessagesVM;
        }
    }
}
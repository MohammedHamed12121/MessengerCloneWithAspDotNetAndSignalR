using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RealTimeChatApp.Models;

namespace RealTimeChatApp.ViewModels
{
    public class UsersWithMessagesViewModel
    {
        public List<Message>? MessagesObject { get; set; }
        public string? curUserId { get; set; }

    }
}
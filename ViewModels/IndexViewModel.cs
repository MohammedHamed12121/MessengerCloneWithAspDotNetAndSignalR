using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RealTimeChatApp.Models;

namespace RealTimeChatApp.ViewModels
{
    public class IndexViewModel
    {
        public IdentityUser? ToUser { get; set; }
        public IdentityUser? FromUser { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
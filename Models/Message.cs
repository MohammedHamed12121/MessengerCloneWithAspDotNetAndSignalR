using Microsoft.AspNetCore.Identity;

namespace RealTimeChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ToUserId { get; set; }
        public IdentityUser? ToUser { get; set; }
        public string? FromUserId { get; set; }
        public IdentityUser? FromUser { get; set; }
    }
}
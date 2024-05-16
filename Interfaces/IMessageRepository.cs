using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeChatApp.Models;

namespace RealTimeChatApp.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllAsync(string userId, string receiverId);
        Message GetById(int id);
        bool Add(Message message);
        bool Update(int id);
        bool Delete(int id);
        bool Save();
    }
}
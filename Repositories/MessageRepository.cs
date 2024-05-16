using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Data;
using RealTimeChatApp.Interfaces;
using RealTimeChatApp.Models;

namespace RealTimeChatApp.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Message message)
        {
            _context.Messages.Add(message);
            return Save();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Message>> GetAllAsync(string userId, string receiverId)
        {
            return await _context.Messages
                               .Where(m => m.FromUserId == userId && m.ToUserId == receiverId
                                        || m.FromUserId == receiverId && m.ToUserId == userId
                               )
                               .OrderBy(m => m.CreatedAt)
                               .Include(m => m.ToUser)
                               .Include(m => m.FromUser)
                               .ToListAsync();
        }

        public Message GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
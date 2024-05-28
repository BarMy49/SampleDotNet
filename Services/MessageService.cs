using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;

namespace SampleDotNet.Services
{
    public class MessageService : IMessageService
    {
        private readonly SiteDbContext _siteDbContext;

        public MessageService(SiteDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

        public async Task<List<Message>> GetMessagesAsync(string userId, string sortOrder)
        {
            var messages = from m in _siteDbContext.Messages
                           where m.ReceiverId == userId
                           select m;
            switch (sortOrder)
            {
                case "oldest":
                    messages = messages.OrderBy(m => m.Timestamp);
                    break;
                case "newest":
                    messages = messages.OrderByDescending(m => m.Timestamp);
                    break;
                default:
                    messages = messages.OrderByDescending(m => m.Timestamp);
                    break;
            }

            return await messages.ToListAsync();
        }

        public async Task<Message> GetMessageAsync(Guid messageId)
        {
            return await _siteDbContext.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .FirstOrDefaultAsync(m => m.messageId == messageId);
        }

        public async Task SendMessageAsync(Message message)
        {
            _siteDbContext.Messages.Add(message);
            await _siteDbContext.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(Guid messageId)
        {
            var message = await _siteDbContext.Messages.FindAsync(messageId);
            _siteDbContext.Messages.Remove(message);
            await _siteDbContext.SaveChangesAsync();
        }   
    }
}

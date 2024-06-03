using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            IQueryable<Message> messages = _siteDbContext.Messages
                .Where(m => m.ReceiverId == userId)
                .Include(m => m.Sender)
                .Include(m => m.Receiver);

            switch (sortOrder)
            {
                case "oldest":
                    messages = messages.OrderBy(m => m.Timestamp);
                    break;
                case "newest":
                default:
                    messages = messages.OrderByDescending(m => m.Timestamp);
                    break;
            }

            return await messages.ToListAsync();
        }

        public async Task SendMessageAsync(Message message)
        {
            _siteDbContext.Messages.Add(message);
            await _siteDbContext.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(Guid messageId)
        {
            var message = await _siteDbContext.Messages.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _siteDbContext.SaveChangesAsync();
            }
        }
    }
}

using SampleDotNet.Models;

namespace SampleDotNet.Interface
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesAsync(string userId, string sortOrder);
        Task SendMessageAsync(Message message);
        Task MarkAsReadAsync(Guid messageId);
    }
}

using SampleDotNet.Models;

namespace SampleDotNet.Interface
{
    public interface IMessageService
    {
        Task SendMessageAsync(Message message);
        Task<List<Message>> GetMessagesAsync(string userId, string sortOrder);
        Task<Message> GetMessageAsync(Guid messageId);
        Task DeleteMessageAsync(Guid messageId);
    }
}

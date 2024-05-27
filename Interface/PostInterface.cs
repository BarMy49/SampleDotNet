using SampleDotNet.Models;

namespace SampleDotNet.Interface
{
    public interface PostInterface
    {
        Task<List<Post>> GetUserPostsAsync(Guid userId);
    }
}

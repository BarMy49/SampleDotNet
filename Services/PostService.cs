using Microsoft.AspNetCore.Identity;
using SampleDotNet.Data;
using SampleDotNet.Models;
using SampleDotNet.Interface;
using Microsoft.EntityFrameworkCore;

namespace SampleDotNet.Services
{
    public class PostService : PostInterface
    {
        private readonly SiteDbContext _context;

        public PostService(SiteDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetUserPostsAsync(Guid userId, string sortOrder = null)
        {
            var userGommunities = await _context.Users
               .OfType<Guser>()
               .Where(u => u.Id == userId.ToString())
               .Include(u => u.Gommunities)
               .FirstOrDefaultAsync();

            if (userGommunities == null)
            {
                return new List<Post>();
            }

            var userGommunityIds = userGommunities.Gommunities.Select(g => g.Id);

            var posts = _context.Posts
                .Include(p => p.Gommunity)
                .Include(p => p.Guser)
                .Include(p => p.Comments)
                .Include(p => p.Reactions)
                .Where(p => userGommunityIds.Contains(p.GommunityId));

            switch (sortOrder)
            {
                case "newest":
                    posts = posts.OrderByDescending(p => p.CreatedAt);
                    break;
                case "oldest":
                    posts = posts.OrderBy(p => p.CreatedAt);
                    break;
                case "best":
                    posts = posts.OrderByDescending(p => p.Gratio);
                    break;              
                case "worst":
                    posts = posts.OrderBy(p => p.Gratio);
                    break;
                default:
                    posts = posts.OrderByDescending(p => p.CreatedAt);
                    break;
            }

            return await posts.ToListAsync();
        }
    }

}

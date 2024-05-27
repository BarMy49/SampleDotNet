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

        public async Task<List<Post>> GetUserPostsAsync(Guid userId)
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

            var posts = await _context.Posts
                .Where(p => userGommunityIds.Contains(p.GommunityId))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return posts;
        }
    }

}

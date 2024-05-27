using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;

namespace SampleDotNet.Services
{
    public class GommunityService : GommunityInterface
    {
        private SiteDbContext _siteDbContext;


        public GommunityService(SiteDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

        public Gommunity GetGommunityByName(string gommunityName)
        {
            return _siteDbContext.Gommunities
                .Include(g => g.Gusers)
                .Include(g => g.Posts)
                    .ThenInclude(p => p.Guser)
                .FirstOrDefault(g => g.GName == gommunityName);
        }

        public void SavePost(Post post, Guser guser)
        {
            post.Gommunity = _siteDbContext.Gommunities
                .Include(g => g.Gusers)
                .Include(g => g.Posts)
                .FirstOrDefault(g => g.Id == post.GommunityId);
            post.Guser = _siteDbContext.Guser.FirstOrDefault(u => u.Id == guser.Id);
            _siteDbContext.Posts.Add(post);
            post.Gommunity.PostCount = post.Gommunity.Posts.Count;
            _siteDbContext.SaveChanges();
        }

        public Post GetPostById(Guid postId)
        {
            return _siteDbContext.Posts
                .Include(p => p.Guser)
                .Include(p => p.Gommunity)
                .Include(p => p.Comments)
                    .ThenInclude(p => p.Guser)
                .FirstOrDefault(p => p.Id == postId);
        }
        public void DeletePost(Guid postId)
        {
            var post = _siteDbContext.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == postId);
            _siteDbContext.Posts.Remove(post);
            _siteDbContext.SaveChanges();
        }
    }
}

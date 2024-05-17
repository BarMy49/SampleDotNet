using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;
using System.Xml.Linq;

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
            return _siteDbContext.Gommunities.Include(g => g.Gusers).Include(g => g.Posts).FirstOrDefault(g => g.GName == gommunityName);
        }

        public async void SavePost(Post post, Guser guser)
        {
            post.Gommunity = _siteDbContext.Gommunities.Include(g => g.Gusers).Include(g => g.Posts).FirstOrDefault(g => g.Id == post.GommunityId);
            post.Guser = _siteDbContext.Guser.FirstOrDefault(u => u.Id == guser.Id);
            _siteDbContext.Posts.Add(post);
            post.Gommunity.PostCount = post.Gommunity.Posts.Count;
            _siteDbContext.SaveChanges();
        }
    }
}

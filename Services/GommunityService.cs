using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}

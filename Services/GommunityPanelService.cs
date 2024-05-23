using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;

namespace SampleDotNet.Services
{
    public class GommunityPanelService : GommunityPanelInterface
    {
        private SiteDbContext _siteDbContext;

        public GommunityPanelService(SiteDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

        public GommunityViewModel ShowGommunityList(string sortOrder)
        {
            var gommunities = from g in _siteDbContext.Gommunities.Include(g => g.Gusers)
                              select g;
            var gommunityModel = new GommunityViewModel();
            switch (sortOrder)
            {
                case "name":
                    gommunities = gommunities.OrderBy(g => g.GName);
                    break;
                case "name_desc":
                    gommunities = gommunities.OrderByDescending(g => g.GName);
                    break;
                case "posts":
                    gommunities = gommunities.OrderBy(g => g.PostCount);
                    break;
                case "posts_desc":
                    gommunities = gommunities.OrderByDescending(g => g.PostCount);
                    break;
                case "gusers":
                    gommunities = gommunities.OrderBy(g => g.GuserCount);
                    break;
                case "gusers_desc":
                    gommunities = gommunities.OrderByDescending(g => g.GuserCount);
                    break;
                default:
                    gommunities = gommunities.OrderByDescending(g => g.GName);
                    break;
            }

            gommunityModel.Gommunities = gommunities.ToList();
            return gommunityModel;
        }
    }
}

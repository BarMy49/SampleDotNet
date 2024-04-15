using Microsoft.AspNetCore.Identity;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;

namespace SampleDotNet.Services
{
    public class CommunityPanelService : CommunityPanelInterface
    {
        private SiteDbContext _siteDbContext;

        public CommunityPanelService(SiteDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

        public CommunityViewModel ShowCommunityList(string sortOrder)
        { 
            var gommunities = from g in _siteDbContext.Communities select g;
            var communityModel = new CommunityViewModel();
            switch (sortOrder)
            {
                case "name":
                    gommunities = gommunities.OrderBy(g => g.GName);
                    break;
                case "name_desc":
                    gommunities = gommunities.OrderByDescending(g => g.GName);
                    break;
                default:
                    gommunities = gommunities.OrderByDescending(g => g.GName);
                    break;
            }
            communityModel.Gommunities = gommunities.ToList();
            return communityModel;
        }
    }
}

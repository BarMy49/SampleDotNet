using Microsoft.AspNetCore.Identity;
namespace SampleDotNet.Models
{
    public class GommunityViewModel
    {
        public List<Gommunity> Gommunities { get; set; }
        public Guser guser { get; set; }
    }
}

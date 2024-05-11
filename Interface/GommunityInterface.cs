using Microsoft.AspNetCore.Mvc;
using SampleDotNet.Models;

namespace SampleDotNet.Interface
{
    public interface GommunityInterface
    {
        public Gommunity GetGommunityByName(string gommunityName);
    }
}

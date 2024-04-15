using SampleDotNet.Models;

namespace SampleDotNet.Interface
{
    public interface CommunityPanelInterface
    {
        public CommunityViewModel ShowCommunityList(string sortOrder);
    }
}

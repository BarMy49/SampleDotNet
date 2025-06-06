using Microsoft.AspNetCore.Identity;
namespace SampleDotNet.Models
{
    public class GuserViewModel
    {
        public List<Guser> Gusers { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}

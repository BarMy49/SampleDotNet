using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleDotNet.Models
{
    public class Guser : IdentityUser
    {
        [DefaultValue(1)]
        public int Garma { get; set; }
        public virtual ICollection<Post>? Posts { get; set;}
        public virtual ICollection<Gommunity>? Gommunities { get; set; }
    }
}

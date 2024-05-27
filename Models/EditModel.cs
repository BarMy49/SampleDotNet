using Microsoft.AspNetCore.Identity;

namespace SampleDotNet.Models
{
    public class EditModel
    {
        public IdentityRole role { get; set; }
        public Guser guser { get; set; }
    }
}

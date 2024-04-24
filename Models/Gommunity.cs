using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SampleDotNet.Models
{
    public class Gommunity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string GName { get; set; }
        public virtual ICollection<Guser>? Gusers { get; set; }
        public int GuserCount { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public int PostCount { get; set; }
    }
}

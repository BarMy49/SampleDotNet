using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SampleDotNet.Models
{
    public class Community
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }
}

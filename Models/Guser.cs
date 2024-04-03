using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleDotNet.Models
{
    public class Guser
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string GNick { get; set; }
        [Required]
        [MaxLength(70)]
        public string GEmail { get; set; }
        [DefaultValue(1)]
        public int Garma { get; set; }
        [Required]
        [MaxLength(128)]
        public string GPassword { get; set; }
        public virtual ICollection<Post>? Posts { get; set;}
        public virtual ICollection<Community>? Communities { get; set; }
    }
}

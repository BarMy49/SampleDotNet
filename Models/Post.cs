using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleDotNet.Models
{
    public class Post
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Image { get; set; }
        [DefaultValue(1)]
        public int Gratio { get; set; }
        [Required]
        [ForeignKey("GuserId")]
        public virtual Guser Guser { get; set; }
        [Required]
        public Guid CommunityId { get; set; }
        public virtual Community Community { get; set; }
    }
}

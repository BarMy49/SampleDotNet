using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleDotNet.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        [Required]
        [DefaultValue(1)]
        public int GoonRatio { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CummunityId { get; set; }
        public virtual Cummunity Cummunity { get; set; }
    }
}

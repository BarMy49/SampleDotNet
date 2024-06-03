using System.ComponentModel.DataAnnotations;
namespace SampleDotNet.Models
{
    public class Comment
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public virtual Guser Guser { get; set; }
        [Required]
        public virtual Post Post { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
namespace SampleDotNet.Models
{
    public class Reaction
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public virtual Guser Guser { get; set; }
        [Required]
        public virtual Post Post { get; set; }
    }
}

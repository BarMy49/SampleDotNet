using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleDotNet.Models
{
    public class Message
    {
        [Key]
        public Guid messageId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual Guser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual Guser Receiver { get; set; }

        public bool IsRead { get; set; }
    }
}
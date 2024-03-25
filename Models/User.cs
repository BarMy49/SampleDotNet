﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleDotNet.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Nick { get; set; }
        [Required]
        [MaxLength(70)]
        public string Email { get; set; }
        [Required]
        [DefaultValue(0)]
        public int RedditKarma { get; set; }
        [Required]
        [MaxLength(128)]
        public string Password { get; set; }
        public virtual ICollection<Post>? Posts { get; set;}
        public virtual ICollection<Cummunity>? Cummunities { get; set; }
    }
}
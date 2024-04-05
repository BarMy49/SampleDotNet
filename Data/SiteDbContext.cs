using SampleDotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace SampleDotNet.Data
{
    public class SiteDbContext : IdentityDbContext
    {
        public SiteDbContext(DbContextOptions<SiteDbContext> options) : base(options)
        {
        
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Community> Communities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Guser>(c =>
            {
                c.Property(u => u.Garma).HasDefaultValue(1);
                c.HasMany(u => u.Communities)
                .WithMany(u => u.Gusers);
                c.HasMany(u => u.Posts)
                .WithOne(u => u.Guser);
            });
            builder.Entity<Post>()
                .Property(e => e.Gratio)
                .HasDefaultValue(1);
        }
        public DbSet<Guser> Guser { get; set; } = default!;
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleDotNet.Models;
namespace SampleDotNet.Data
{
    public class SiteDbContext : IdentityDbContext
    {
        public SiteDbContext(DbContextOptions<SiteDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Gommunity> Gommunities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Guser>(c =>
            {
                c.Property(u => u.Garma).HasDefaultValue(1);
                c.HasIndex(u => u.NormalizedEmail).IsUnique();
                c.HasMany(u => u.Gommunities)
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

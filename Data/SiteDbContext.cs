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
                c.HasMany(u => u.Comments)
                    .WithOne(u => u.Guser)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Post>(e => 
            {
                e.Property(p => p.Gratio).HasDefaultValue(1);
                e.HasMany(p => p.Comments)
                    .WithOne(c => c.Post)
                    .OnDelete(DeleteBehavior.NoAction);
                e.Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Comment>(c =>
            {
                c.Property(c => c.CreatedAt).HasDefaultValueSql("getdate()");
            });
        }
        public DbSet<Guser> Guser { get; set; } = default!;
    }
}

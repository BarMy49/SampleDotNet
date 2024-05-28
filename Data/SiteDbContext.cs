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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
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
                    .WithOne(u => u.Guser)
                        .OnDelete(DeleteBehavior.Cascade);
                c.HasMany(u => u.Comments)
                    .WithOne(u => u.Guser)
                        .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Post>(e => 
            {
                e.Property(p => p.Gratio).HasDefaultValue(1);
                e.Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
                e.HasMany(p => p.Reactions)
                    .WithOne(r => r.Post)
                        .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(p => p.Comments)
                    .WithOne(c => c.Post)
                        .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Comment>(c =>
            {
                c.Property(c => c.CreatedAt).HasDefaultValueSql("getdate()");
                c.HasOne(c => c.Guser)
                    .WithMany(u => u.Comments)
                        .OnDelete(DeleteBehavior.Restrict);
                c.HasOne(c => c.Post)
                    .WithMany(p => p.Comments)
                        .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Reaction>(r =>
            {
                r.HasOne(r => r.Guser)
                    .WithMany(u => u.Reactions)
                        .OnDelete(DeleteBehavior.Restrict);
                r.HasOne(r => r.Post)
                    .WithMany(p => p.Reactions)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
        }
        public DbSet<Guser> Guser { get; set; } = default!;
    }
}

using SampleDotNet.Models;
using Microsoft.EntityFrameworkCore;
namespace SampleDotNet.Data
{
    public class SiteDbContext : DbContext
    {
        public SiteDbContext(DbContextOptions options) : base(options)
        { 
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Community> Communities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Communities)
                .WithMany(e => e.Users);
            modelBuilder.Entity<User>()
                .Property(e => e.Garma)
                .HasDefaultValue(1);
            modelBuilder.Entity<Post>()
                .Property(e => e.Gratio)
                .HasDefaultValue(1);
        }
    }
}

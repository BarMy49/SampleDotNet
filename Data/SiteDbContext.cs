using SampleDotNet.Models;
using Microsoft.EntityFrameworkCore;
namespace SampleDotNet.Data
{
    public class SiteDbContext : SampleDotNetContext
    {
        public SiteDbContext(DbContextOptions options) : base(options)
        { 
        
        }
        public DbSet<Guser> Gusers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Community> Communities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Guser>()
                .HasMany(e => e.Communities)
                .WithMany(e => e.Gusers);
            modelBuilder.Entity<Guser>()
                .Property(e => e.Garma)
                .HasDefaultValue(1);
            modelBuilder.Entity<Post>()
                .Property(e => e.Gratio)
                .HasDefaultValue(1);
        }
    }
}

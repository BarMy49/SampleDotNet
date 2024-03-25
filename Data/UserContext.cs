using SampleDotNet.Models;
using Microsoft.EntityFrameworkCore;
namespace SampleDotNet.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        { 
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Cummunity> Cummunities { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Cummunities)
                .WithMany(e => e.Users);
            
        }
    }
}

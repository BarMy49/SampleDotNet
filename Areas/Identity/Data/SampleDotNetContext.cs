using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SampleDotNet.Data;

public class SampleDotNetContext : IdentityDbContext<IdentityUser>
{
    public SampleDotNetContext(DbContextOptions<SampleDotNetContext> options)
        : base(options)
    {
    }

    protected SampleDotNetContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount)
                                      .Ignore(c => c.TwoFactorEnabled)
                                      .Ignore(c => c.LockoutEnabled)
                                      .Ignore(c => c.LockoutEnd)
                                      .Ignore(c => c.PhoneNumber)
                                      .Ignore(c => c.PhoneNumberConfirmed);
    }
}

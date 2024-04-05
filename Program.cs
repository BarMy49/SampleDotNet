using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleDotNet.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
    {
        options.Cookie.HttpOnly = true;
    });

builder.Services.AddDbContext<SiteDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString
    ("SampleDotNetDB")));

builder.Services.AddIdentity<Guser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<SiteDbContext>()
    .AddDefaultUI()
    .AddRoles<IdentityRole>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using(var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Owner", "Moderator", "Guser" };
    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Guser>>();
    string email = "Grzegorz@admin.com";
    string password = "zaq1@WSX";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new Guser();
        user.UserName = "Owner";
        user.Email = email;
        user.EmailConfirmed = true;
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Owner");
    }
}

app.MapRazorPages();

app.Run();

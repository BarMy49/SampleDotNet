using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;
using SampleDotNet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<GuserPanelInterface, GuserPanelService>();
builder.Services.AddTransient<GommunityPanelInterface, GommunityPanelService>();
builder.Services.AddTransient<GommunityInterface, GommunityService>();
builder.Services.AddTransient<PostInterface, PostService>();
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
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<SiteDbContext>()
    .AddDefaultUI()
    .AddTokenProvider<DataProtectorTokenProvider<Guser>>(TokenOptions.DefaultProvider)
    .AddRoles<IdentityRole>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
    });

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
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
       name: "Gommunity",
       pattern: "{controller=Gommunity}/{gommunityName?}",
       defaults: new { controller = "Gommunity", action = "Index" }
);

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Owner", "Moderator", "Guser" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Guser>>();
    string username = "Goorzechu";
    string email = "Grzegorz@admin.com";
    string password = "zaq1@WSX";
    if (await userManager.FindByNameAsync(username) == null)
    {
        var user = new Guser();
        user.UserName = username;
        user.Email = email;
        user.EmailConfirmed = true;
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Owner");
    }
}

app.MapRazorPages();

app.Run();

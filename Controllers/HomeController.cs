using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;
using SampleDotNet.Services;
using System.Diagnostics;

namespace SampleDotNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SiteDbContext _context;
        private UserManager<Guser> _userManager;
        private PostInterface _postInterface;

        public HomeController(ILogger<HomeController> logger, SiteDbContext context, PostInterface postInterface, UserManager<Guser> userManager)
        {
            _logger = logger;
            _context = context;
            _postInterface = postInterface;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return View("Hello");
            }

            var id = await _userManager.GetUserAsync(User);

            if(id == null)
            {
                return View("Hello");
            }

            var guserId = Guid.Parse(id.Id);
            var posts = await _postInterface.GetUserPostsAsync(guserId, sortOrder);

            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

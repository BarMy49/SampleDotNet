using Microsoft.AspNetCore.Mvc;
using SampleDotNet.Models;
using System.Diagnostics;
using SampleDotNet.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SampleDotNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _context;

        public HomeController(ILogger<HomeController> logger, UserContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
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

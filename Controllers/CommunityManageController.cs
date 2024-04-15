using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Models;
using System.Xml.Linq;

namespace SampleDotNet.Controllers
{
    public class CommunityManageController : Controller
    {
        private SiteDbContext _context;
        public CommunityManageController(SiteDbContext context)
        {
            _context = context;
        }

        public IActionResult Gommunity()
        {
            return View();
        }

        public IActionResult GommunityAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public IActionResult Add(Community addCommunity)
        {
            var community = new Community
            {
                GName = addCommunity.GName
            };
            _context.Communities.Add(community);
            _context.SaveChanges();
            return View("Gommunity");
        }
    }
}

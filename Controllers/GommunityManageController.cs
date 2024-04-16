using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;
using System.Xml.Linq;

namespace SampleDotNet.Controllers
{
    public class GommunityManageController : Controller
    {
        private SiteDbContext _context;
        private GommunityPanelInterface _gommunityInterface;
        public GommunityManageController(SiteDbContext context, GommunityPanelInterface gommunityInterface)
        {
            _context = context;
            _gommunityInterface = gommunityInterface;
        }

        public IActionResult GommunityList(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            var gommunityModel = _gommunityInterface.ShowGommunityList(sortOrder);
            return View(gommunityModel);
        }

        public IActionResult GommunityAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public IActionResult Add(Gommunity addGommunity)
        {
            var gommunity = new Gommunity
            {
                GName = addGommunity.GName
            };
            _context.Gommunities.Add(gommunity);
            _context.SaveChanges();
            return View("GommunityList");
        }
    }
}

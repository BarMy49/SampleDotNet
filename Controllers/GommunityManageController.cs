using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<Guser> _userManager;
        public GommunityManageController(SiteDbContext context, UserManager<Guser> userManager, GommunityPanelInterface gommunityInterface)
        {
            _context = context;
            _userManager = userManager;
            _gommunityInterface = gommunityInterface;
        }

        public async Task<IActionResult> GommunityList(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            var gommunityModel = _gommunityInterface.ShowGommunityList(sortOrder);
            gommunityModel.guser = await _userManager.GetUserAsync(User);

            return View(gommunityModel);
        }

        public IActionResult GommunityAdd()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Join(string id)
        {
            if (!Guid.TryParse(id, out Guid gommunityId))
            {
                return BadRequest("Invalid Gommunity id");
            }

            var gommunity = _context.Gommunities
                .Include(g => g.Gusers)
                .FirstOrDefault(g => g.Id == gommunityId);
            if (gommunity == null)
            {
                return NotFound("Gommunity not found");
            }

            var guser = await _userManager.GetUserAsync(User);
            if (guser == null)
            {
                return NotFound("Guser not found");
            }

            if (!gommunity.Gusers.Contains(guser))
            {
                gommunity.Gusers.Add(guser);
                gommunity.GuserCount = gommunity.Gusers.Count;
            }

            _context.SaveChanges();

            return RedirectToAction("GommunityList");
        }
        [Authorize]
        public async Task<IActionResult> Leave(string id)
        {
            if (!Guid.TryParse(id, out Guid gommunityId))
            {
                return BadRequest("Invalid Gommunity id");
            }

            var gommunity = _context.Gommunities
                .Include(g => g.Gusers)
                .FirstOrDefault(g => g.Id == gommunityId);
            if (gommunity == null)
            {
                return NotFound("Gommunity not found");
            }

            var guser = await _userManager.GetUserAsync(User);
            if (guser == null)
            {
                return NotFound("Guser not found");
            }

            if (gommunity.Gusers.Contains(guser))
            {
                gommunity.Gusers.Remove(guser);
                gommunity.GuserCount = gommunity.Gusers.Count;
            }

            _context.SaveChanges();

            return RedirectToAction("GommunityList");
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
            return RedirectToAction("GommunityList");
        }
    }
}

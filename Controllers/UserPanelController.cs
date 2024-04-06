using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleDotNet.Data;
using SampleDotNet.Models;

namespace SampleDotNet.Controllers
{
    public class UserPanelController : Controller
    {
        private UserManager<Guser> _userManager;
        private SiteDbContext _siteDbContext;

        public UserPanelController(UserManager<Guser> userManager, SiteDbContext siteDbContext)
        {
            _userManager = userManager;
            _siteDbContext = siteDbContext;
        }

        public  IActionResult UserList(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.EmailSort = sortOrder == "email" ? "email_desc" : "email";
            var gusers = from u in _siteDbContext.Guser select u;
            switch (sortOrder)
            {
                case "name":
                    gusers = gusers.OrderBy(u => u.UserName);
                    break;
                case "name_desc":
                    gusers = gusers.OrderByDescending(u => u.UserName);
                    break;
                case "email":
                    gusers = gusers.OrderBy(u => u.Email);
                    break;
                case "email_desc":
                    gusers = gusers.OrderByDescending(u => u.Email);
                    break;
                default:
                    gusers = gusers.OrderByDescending(u => u.UserName);
                    break;
            }
            return View(gusers.ToList());
        }
    }
}

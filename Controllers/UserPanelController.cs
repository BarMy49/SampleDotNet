using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using SampleDotNet.Data;
using SampleDotNet.Models;
using System;

namespace SampleDotNet.Controllers
{
    [Authorize(Roles="Owner")]
    public class UserPanelController : Controller
    {
        private UserManager<Guser> _userManager;
        private SiteDbContext _siteDbContext;

        public UserPanelController(UserManager<Guser> userManager, SiteDbContext siteDbContext)
        {
            _userManager = userManager;
            _siteDbContext = siteDbContext;
        }

        public IActionResult UserList(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.EmailSort = sortOrder == "email" ? "email_desc" : "email";
            var gusers = from u in _siteDbContext.Guser select u;
            var userModel = new UserViewModel();
            switch (sortOrder)
            {
                case "name":
                    gusers = gusers.OrderBy(u => u.UserName);
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
            userModel.Gusers = gusers.ToList();
            return View(userModel);
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Edit(string id)
        {
            var editModel = new EditModel();
            editModel.guser = _siteDbContext.Guser.Find(id);
            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditModel editModel)
        {
            var guser = _siteDbContext.Guser.Find(editModel.guser.Id);
            var isRoleOwner = await _userManager.IsInRoleAsync(guser, "Owner");
            var oldRole = await _userManager.GetRolesAsync(guser);
            if (guser != null && isRoleOwner == false)
            {
                try
                {
                    guser.UserName = editModel.guser.UserName;
                    guser.Email = editModel.guser.Email;
                    guser.NormalizedEmail = _userManager.NormalizeEmail(editModel.guser.Email);
                    guser.NormalizedUserName = _userManager.NormalizeName(editModel.guser.UserName);
                    var isRoleSame = await _userManager.IsInRoleAsync(guser, editModel.role.Name);
                    if (isRoleSame == false)
                    {
                        await _userManager.AddToRoleAsync(guser, editModel.role.Name);
                        await _userManager.RemoveFromRoleAsync(guser, oldRole[0]);
                    }
                    _siteDbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "UserPanel");
                }
            }
            return RedirectToAction("UserList","UserPanel");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EditModel editModel)
        {
            var guser = _siteDbContext.Guser.Find(editModel.guser.Id);
            var isRoleOwner = await _userManager.IsInRoleAsync(guser, "Owner");
            if (guser != null && isRoleOwner == false)
            {
                _siteDbContext.Guser.Remove(guser);
                await _siteDbContext.SaveChangesAsync();
            }
            return RedirectToAction("UserList", "UserPanel");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;
using System;

namespace SampleDotNet.Controllers
{
    [Authorize(Roles="Owner")]
    public class GuserPanelController : Controller
    {
        private UserManager<Guser> _userManager;
        private UserPanelInterface _userInterface;
        public GuserPanelController(UserManager<Guser> userManager, UserPanelInterface userInterface)
        {
            _userManager = userManager;
            _userInterface = userInterface;
        }

        public IActionResult GuserList(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.EmailSort = sortOrder == "email" ? "email_desc" : "email";
            var userModel = _userInterface.ShowUserList(sortOrder);
            return View(userModel);
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Edit(string id)
        {
            var editModel = _userInterface.ShowEdit(id);
            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditModel editModel)
        {
            try
            {
                await _userInterface.EditUserList(editModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "UserPanel");
            }
            return RedirectToAction("UserList", "UserPanel");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EditModel editModel)
        {
            await _userInterface.DeleteUserList(editModel);
            return RedirectToAction("UserList", "UserPanel");
        }
    }
}

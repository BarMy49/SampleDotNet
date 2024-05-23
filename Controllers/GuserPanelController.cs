using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleDotNet.Interface;
using SampleDotNet.Models;

namespace SampleDotNet.Controllers
{
    [Authorize(Roles = "Owner")]
    public class GuserPanelController : Controller
    {
        private UserManager<Guser> _guserManager;
        private GuserPanelInterface _guserInterface;
        public GuserPanelController(UserManager<Guser> userManager, GuserPanelInterface userInterface)
        {
            _guserManager = userManager;
            _guserInterface = userInterface;
        }

        public IActionResult GuserList(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.EmailSort = sortOrder == "email" ? "email_desc" : "email";
            var userModel = _guserInterface.ShowUserList(sortOrder);
            return View(userModel);
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Edit(string id)
        {
            var editModel = _guserInterface.ShowEdit(id);
            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditModel editModel)
        {
            try
            {
                await _guserInterface.EditUserList(editModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "GuserPanel");
            }
            return RedirectToAction("GuserList", "GuserPanel");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EditModel editModel)
        {
            await _guserInterface.DeleteUserList(editModel);
            return RedirectToAction("GuserList", "GuserPanel");
        }
    }
}

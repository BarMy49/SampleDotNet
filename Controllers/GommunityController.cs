using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleDotNet.Interface;
using SampleDotNet.Models;

namespace SampleDotNet.Controllers
{
    public class GommunityController : Controller
    {
        private GommunityInterface _gommunityInterface;
        private UserManager<Guser> _userManager;

        public GommunityController(GommunityInterface gommunityInterface, UserManager<Guser> userManager)
        {
            _gommunityInterface = gommunityInterface;
            _userManager = userManager;
        }

        public IActionResult Index(string gommunityName)
        {
            var gommunity = _gommunityInterface.GetGommunityByName(gommunityName);
            if(gommunity == null)
            {
                return NotFound();
            }

            return View("Gommunity", gommunity);
        }

        public async Task<IActionResult> CreatePost(string gommunityName)
        { 
            var postModel = new Post();
            postModel.Gommunity = _gommunityInterface.GetGommunityByName(gommunityName);
            return View(postModel);
        }
        [HttpPost]
        public async Task<IActionResult> SavePost(Post post)
        {
            var guser = await _userManager.GetUserAsync(User);
            _gommunityInterface.SavePost(post, guser);
            return RedirectToAction("Index", new { gommunityName = post.Gommunity.GName });
        }
    }
}

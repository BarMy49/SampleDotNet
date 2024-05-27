using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleDotNet.Interface;
using SampleDotNet.Models;
using SixLabors.ImageSharp;

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
            if (gommunity == null)
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
        public async Task<IActionResult> SavePost(Post post, IFormFile image)
        {
            var guser = await _userManager.GetUserAsync(User);

            if (image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    if (image.ContentType == "image/png")
                    {
                        using (var ms = new MemoryStream(imageBytes))
                        using (var output = new MemoryStream())
                        {
                            var img = SixLabors.ImageSharp.Image.Load(ms);
                            img.SaveAsJpeg(output);
                            imageBytes = output.ToArray();
                        }
                    }

                    post.Image = Convert.ToBase64String(imageBytes);
                }
            }

            _gommunityInterface.SavePost(post, guser);
            return RedirectToAction("Index", new { gommunityName = post.Gommunity.GName });
        }
    }
}

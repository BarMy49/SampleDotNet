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

        public IActionResult Index(string gommunityName, [FromForm] string sortOrder)
        {
            var gommunity = _gommunityInterface.GetGommunityByName(gommunityName);
            if (gommunity == null)
            {
                return NotFound();
            }

            switch (sortOrder)
            {
                case "newest":
                    gommunity.Posts = gommunity.Posts.OrderByDescending(p => p.CreatedAt).ToList();
                    break;
                case "oldest":
                    gommunity.Posts = gommunity.Posts.OrderBy(p => p.CreatedAt).ToList();
                    break;
                case "best":
                    gommunity.Posts = gommunity.Posts.OrderByDescending(p => p.Gratio).ToList();
                    break;
                case "worst":
                    gommunity.Posts = gommunity.Posts.OrderBy(p => p.Gratio).ToList();
                    break;
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
        public IActionResult ViewPost(Guid postId)
        {
            var post = _gommunityInterface.GetPostById(postId);
            return View(post);
        }
        public IActionResult Delete(Guid postId) {
            var post = _gommunityInterface.GetPostById(postId);
            var gommunity = post.Gommunity;
            _gommunityInterface.DeletePost(postId);
            return View("Gommunity", gommunity);
        }
        public async Task<IActionResult> Reaction(Guid postId, int value)
        {
            var guser = await _userManager.GetUserAsync(User);
            var post = _gommunityInterface.GetPostById(postId);
            var reaction = new Reaction
            {
                Guser = guser,
                Post = post,
                Value = value
            };

            post.Gratio = post.Gratio + value;
            _gommunityInterface.SaveReaction(reaction);

            return RedirectToAction("ViewPost", new { postId = postId });
        }
        public async Task<IActionResult> UndoReaction(Guid postId, int value)
        {
            var guser = await _userManager.GetUserAsync(User);
            var post = _gommunityInterface.GetPostById(postId);
            var reaction = new Reaction
            {
                Guser = guser,
                Post = post,
                Value = value
            };

            post.Gratio = post.Gratio - value;
            _gommunityInterface.DeleteReaction(reaction);

            return RedirectToAction("ViewPost", new { postId = postId });
        }
        public async Task<IActionResult> CreateComment([FromForm] Guid postId, [FromForm] string comment)
        {
            var guser = await _userManager.GetUserAsync(User);
            var post = _gommunityInterface.GetPostById(postId);
            var newComment = new Comment
            {
                Guser = guser,
                Post = post,
                Content = comment
            };

            post.Comments.Add(newComment);
            _gommunityInterface.AddComment(newComment);

            return RedirectToAction("ViewPost", new { postId = postId });
        }
        public IActionResult DeleteComment(Guid commentId, Guid postId)
        {
            _gommunityInterface.DeleteComment(commentId);
            return RedirectToAction("ViewPost", new { postId = postId });
        }
    }
}

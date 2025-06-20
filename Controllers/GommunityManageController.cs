﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SampleDotNet.Data;
using SampleDotNet.Interface;
using SampleDotNet.Models;
using SixLabors.ImageSharp;

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

        public IActionResult Redirect(string gommunityName)
        {

            return RedirectToRoute("Gommunity", new { controller = "Gommunity", action = "Index", gommunityName = gommunityName });
        }

        public async Task<IActionResult> GommunityList(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.PostsSort = sortOrder == "posts" ? "posts_desc" : "posts";
            ViewBag.GusersSort = sortOrder == "gusers" ? "gusers_desc" : "gusers";
            var gommunityModel = _gommunityInterface.ShowGommunityList(sortOrder);

            return View(gommunityModel);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult GommunityAdd()
        {
            return View();
        }

        [Authorize(Roles = "Owner")]
        public IActionResult GommunityEdit(string id)
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

            return View(gommunity);
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
        public async Task<IActionResult> Add(Gommunity addGommunity, IFormFile banner)
        {
            var gommunity = new Gommunity
            {
                GName = addGommunity.GName
            };

            if (banner != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await banner.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    if (banner.ContentType == "image/png")
                    {
                        using (var ms = new MemoryStream(imageBytes))
                        using (var output = new MemoryStream())
                        {
                            var img = SixLabors.ImageSharp.Image.Load(ms);
                            img.SaveAsJpeg(output);
                            imageBytes = output.ToArray();
                        }
                    }

                    gommunity.Banner = Convert.ToBase64String(imageBytes);
                }
            }
            _context.Gommunities.Add(gommunity);
            _context.SaveChanges();
            return RedirectToAction("GommunityList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public IActionResult Edit(Gommunity editGommunity)
        {
            var gommunity = _context.Gommunities
                .Include(g => g.Gusers)
                .Include(g => g.Posts)
                .FirstOrDefault(g => g.Id == editGommunity.Id);
            if (gommunity == null)
            {
                return NotFound("Gommunity not found");
            }

            gommunity.GName = editGommunity.GName;
            _context.SaveChanges();
            return RedirectToAction("GommunityList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public IActionResult Delete(Gommunity editGommunity)
        {
            var gommunity = _context.Gommunities
                .Include(g => g.Gusers)
                .Include(g => g.Posts)
                .FirstOrDefault(g => g.Id == editGommunity.Id);
            if (gommunity == null)
            {
                return NotFound("Gommunity not found");
            }

            gommunity.Gusers.Clear();

            _context.Gommunities.Remove(gommunity);
            _context.SaveChanges();
            return RedirectToAction("GommunityList");}

    }
}

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
    public class CommunityPanelController : Controller
    {
        private CommunityPanelInterface _communityInterface;

        public CommunityPanelController(CommunityPanelInterface communityInterface)
        {
            _communityInterface = communityInterface;
        }

        public IActionResult CommunityList(string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            var communityModel = _communityInterface.ShowCommunityList(sortOrder);
            return View(communityModel);
        }
    }
}

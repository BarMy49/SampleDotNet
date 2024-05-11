using Microsoft.AspNetCore.Mvc;
using SampleDotNet.Interface;

namespace SampleDotNet.Controllers
{
    public class GommunityController : Controller
    {
        private GommunityInterface _gommunityInterface;

        public GommunityController(GommunityInterface gommunityInterface)
        {
            _gommunityInterface = gommunityInterface;
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
    }
}

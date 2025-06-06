using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SampleDotNet.Interface;
using SampleDotNet.Models;

namespace SampleDotNet.Controllers
{
    public class MessageController : Controller
    {
        private readonly UserManager<Guser> _userManager;
        private readonly IMessageService _messageService;

        public MessageController(UserManager<Guser> userManager, IMessageService messageService)
        {
            _userManager = userManager;
            _messageService = messageService;
        }

        public async Task<IActionResult> SendMessage(string receiverUsername)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Hello", "Home");
            }

            ViewBag.ReceiverUsername = receiverUsername;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendMessage(string receiverUsername, string content)
        {
            if (string.IsNullOrEmpty(receiverUsername) || string.IsNullOrEmpty(content))
            {
                return BadRequest("Receiver username and content cannot be null or empty.");
            }

            var sender = await _userManager.GetUserAsync(User);
            var receiver = await _userManager.FindByNameAsync(receiverUsername);

            if (receiver == null || sender == null)
            {
                return NotFound();
            }

            var message = new SampleDotNet.Models.Message
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Content = content,
                Timestamp = DateTime.UtcNow
            };

            await _messageService.SendMessageAsync(message);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return RedirectToAction("Hello", "Home");
            }

            var messages = await _messageService.GetMessagesAsync(user.Id, sortOrder);

            return View("Inbox", messages);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(Guid messageId)
        {
            await _messageService.MarkAsReadAsync(messageId);
            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using LetThereBeVoice.Data;
using LetThereBeVoice.Models;
using Microsoft.EntityFrameworkCore;

namespace LetThereBeVoice.Controllers
{
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult List(int channelId)
        {
            var channel = _context.Channels.FirstOrDefault(c => c.ChannelID == channelId);
            var messages = _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.ChannelID == channelId)
                .OrderBy(m => m.SentAt)
                .ToList();

            ViewBag.Channel = channel;
            return View(messages);
        }

        [HttpPost]
        public IActionResult Send(int channelId, string content)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            var message = new Message
            {
                Content = content,
                SentAt = DateTime.UtcNow,
                ChannelID = channelId,
                SenderID = userId.Value
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("List", new { channelId });
        }
    }
}


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
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            var channel = _context.Channels.FirstOrDefault(c => c.ChannelID == channelId);
            var messages = _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.ChannelID == channelId)
                .OrderBy(m => m.SentAt)
                .ToList();

            ViewBag.Channel = channel;
            ViewBag.UserID = userId;

            var currentVoiceChannelId = HttpContext.Session.GetInt32("VoiceChannelID");
            ViewBag.JoinedVoice = currentVoiceChannelId == channelId;

            var participants = _context.UserServer
                .Where(us => us.ServerID == channel.ServerID)
                .Select(us => us.User)
                .ToList();

            ViewBag.Participants = participants;

            return View(messages);
        }


        [HttpPost]
        public IActionResult Send(int channelId, string content)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            var message = new Message
            {
                ChannelID = channelId,
                SenderID = userId.Value,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);

            var channel = _context.Channels.FirstOrDefault(c => c.ChannelID == channelId);
            if (channel != null)
            {
                channel.LastActivity = DateTime.UtcNow;
            }

            _context.SaveChanges();

            return RedirectToAction("List", new { channelId });
        }

        public IActionResult Edit(int id)
        {
            var msg = _context.Messages.Find(id);
            if (msg == null) return NotFound();
            return View(msg);
        }

        [HttpPost]
        public IActionResult Edit(Message updated)
        {
            var existing = _context.Messages.Find(updated.MessageID);
            if (existing == null) return NotFound();

            existing.Content = updated.Content;
            _context.SaveChanges();

            return RedirectToAction("List", new { channelId = existing.ChannelID });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var msg = _context.Messages.Find(id);
            if (msg == null) return NotFound();

            int channelId = msg.ChannelID;
            _context.Messages.Remove(msg);
            _context.SaveChanges();

            return RedirectToAction("List", new { channelId });
        }
        

        [HttpPost]
        public IActionResult JoinVoice(int channelId)
        {
            HttpContext.Session.SetInt32("VoiceChannelID", channelId);
            return RedirectToAction("List", new { channelId });
        }

        [HttpPost]
        public IActionResult LeaveVoice()
        {
            HttpContext.Session.Remove("VoiceChannelID");
            return RedirectToAction("Index", "Home");
        }

    }

}


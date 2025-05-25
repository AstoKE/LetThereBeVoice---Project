using LetThereBeVoice.Data;
using LetThereBeVoice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class VoiceController : Controller
{
    private readonly AppDbContext _context;

    public VoiceController(AppDbContext context)
    {
        _context = context;
    }

    // Sesli sohbete katıl
    public IActionResult Join(int channelId)
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null) return RedirectToAction("Login", "Account");

        var existing = _context.VoiceSessions
            .FirstOrDefault(v => v.UserID == userId && v.ChannelID == channelId);

        if (existing == null)
        {
            var session = new VoiceSession
            {
                ChannelID = channelId,
                UserID = userId.Value,
                JoinedAt = DateTime.UtcNow
            };

            _context.VoiceSessions.Add(session);
            _context.SaveChanges();
        }

        return RedirectToAction("List", "Message", new { channelId });
    }

    // Sesten ayrıl
    public IActionResult Leave(int channelId)
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null) return RedirectToAction("Login", "Account");

        var session = _context.VoiceSessions
            .FirstOrDefault(v => v.UserID == userId && v.ChannelID == channelId);

        if (session != null)
        {
            _context.VoiceSessions.Remove(session);
            _context.SaveChanges();
        }

        return RedirectToAction("List", "Message", new { channelId });
    }
}

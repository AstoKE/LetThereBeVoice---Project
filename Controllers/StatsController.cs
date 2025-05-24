using LetThereBeVoice.Data;
using Microsoft.AspNetCore.Mvc;
using LetThereBeVoice.Models.ViewModels;

public class StatsController : Controller
{
    private readonly AppDbContext _context;

    public StatsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult ChannelMessageCounts()
    {
        var data = _context.Messages
            .GroupBy(m => m.ChannelID)
            .Select(g => new ChannelMessageCountViewModel
            {
                ChannelID = g.Key,
                MessageCount = g.Count(),
                ChannelName = _context.Channels
                    .Where(c => c.ChannelID == g.Key)
                    .Select(c => c.ChannelName)
                    .FirstOrDefault(),
                ServerMemberCount = _context.UserServer
                    .Where(us => us.ServerID == _context.Channels
                        .Where(c => c.ChannelID == g.Key)
                        .Select(c => c.ServerID)
                        .FirstOrDefault())
                    .Count(),
                AverageMessageLength = g.Average(m => m.Content.Length),
                LastMessageTime = g.Max(m => m.SentAt)
            }).ToList();

        return View(data);
    }


    public IActionResult UserMessageStats()
    {
        var data = _context.Messages
            .GroupBy(m => m.SenderID)
            .Select(g => new UserMessageStatsViewModel
            {
                UserID = g.Key,
                Username = _context.Users
                    .Where(u => u.UserID == g.Key)
                    .Select(u => u.Username)
                    .FirstOrDefault(),
                MessageCount = g.Count()
            }).ToList();

        return View(data);
    }

}

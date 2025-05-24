using LetThereBeVoice.Data;
using Microsoft.AspNetCore.Mvc;
using LetThereBeVoice.Models;

public class ChannelController : Controller
{
    private readonly AppDbContext _context;

    public ChannelController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult List(int serverId)
    {
        var channels = _context.Channels
            .Where(c => c.ServerID == serverId)
            .ToList();

        ViewBag.ServerID = serverId;
        return View(channels);
    }

    [HttpPost]
    public IActionResult Create(int serverId, string channelName)
    {
        var channel = new Channel
        {
            ChannelName = channelName,
            ChannelType = "Text",
            ServerID = serverId,
            CreatedDate = DateTime.UtcNow
        };

        _context.Channels.Add(channel);
        _context.SaveChanges();

        return RedirectToAction("List", new { serverId });
    }
}

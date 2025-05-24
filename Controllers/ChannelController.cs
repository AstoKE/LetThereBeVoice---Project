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

        return View(channels); // ✅ BU satır çok önemli
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
    // GET: /Channel/Edit
    public IActionResult Edit(int id)
    {
        var channel = _context.Channels.Find(id);
        if (channel == null) return NotFound();
        return View(channel);
    }

    [HttpPost]
    public IActionResult Edit(Channel updated)
    {
        var existing = _context.Channels.Find(updated.ChannelID);
        if (existing == null) return NotFound();

        existing.ChannelName = updated.ChannelName;
        _context.SaveChanges();
        return RedirectToAction("List", new { serverId = existing.ServerID });
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var channel = _context.Channels.Find(id);
        if (channel == null) return NotFound();

        int serverId = channel.ServerID;
        _context.Channels.Remove(channel);
        _context.SaveChanges();

        return RedirectToAction("List", new { serverId });
    }

}

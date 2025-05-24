using LetThereBeVoice.Data;
using LetThereBeVoice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class FriendController : Controller
{
    private readonly AppDbContext _context;

    public FriendController(AppDbContext context)
    {
        _context = context;
    }

    // 📄 Tüm kullanıcıları göster, arkadaş değilse istek atılabilir
    public IActionResult List()
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null) return RedirectToAction("Login", "Account");

        var existingIds = _context.Friendships
            .Where(f => (f.RequesterId == userId || f.ReceiverId == userId))
            .Select(f => f.RequesterId == userId ? f.ReceiverId : f.RequesterId)
            .ToList();

        var users = _context.Users
            .Where(u => u.UserID != userId && !existingIds.Contains(u.UserID))
            .ToList();

        return View(users);
    }

    // 📩 Arkadaşlık isteği gönder
    public IActionResult Send(int id)
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null) return RedirectToAction("Login", "Account");

        var request = new Friendship
        {
            RequesterId = userId.Value,
            ReceiverId = id,
            Status = "Pending",
            RequestedAt = DateTime.UtcNow
        };

        _context.Friendships.Add(request);
        _context.SaveChanges();

        return RedirectToAction("List");
    }

    // 📥 Gelen istekleri gör
    public IActionResult Requests()
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null) return RedirectToAction("Login", "Account");

        var incoming = _context.Friendships
            .Include(f => f.Requester)
            .Where(f => f.ReceiverId == userId && f.Status == "Pending")
            .ToList();

        return View(incoming);
    }

    // ✅ İsteği kabul et
    public IActionResult Accept(int id)
    {
        var request = _context.Friendships.Find(id);
        if (request != null) request.Status = "Accepted";
        _context.SaveChanges();
        return RedirectToAction("Requests");
    }

    // ❌ Reddet
    public IActionResult Reject(int id)
    {
        var request = _context.Friendships.Find(id);
        if (request != null) request.Status = "Rejected";
        _context.SaveChanges();
        return RedirectToAction("Requests");
    }

    // 👥 Kabul edilmiş arkadaşları gör
    public IActionResult Friends()
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null) return RedirectToAction("Login", "Account");

        var friendIds = _context.Friendships
            .Where(f => f.Status == "Accepted" && (f.RequesterId == userId || f.ReceiverId == userId))
            .Select(f => f.RequesterId == userId ? f.ReceiverId : f.RequesterId)
            .ToList();

        var friends = _context.Users
            .Where(u => friendIds.Contains(u.UserID))
            .ToList();

        return View(friends);
    }
}

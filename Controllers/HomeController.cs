using Microsoft.AspNetCore.Mvc;
using LetThereBeVoice.Data;
using Microsoft.EntityFrameworkCore;

namespace LetThereBeVoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            var user = _context.Users
                .Include(u => u.CreatedServers)
                .FirstOrDefault(u => u.UserID == userId);

            var joinedServerIds = _context.UserServer
            .Where(us => us.UserID == userId)
            .Select(us => us.ServerID)
            .ToList();

            var allServers = _context.Servers.ToList(); 
            var joinedServers = allServers
                .Where(s => joinedServerIds.Contains(s.ServerID))
                .ToList();

            var userRoleMap = _context.ServerRoles
                .Include(sr => sr.Role)
                .Where(sr => joinedServerIds.Contains(sr.ServerID) && sr.UserID == userId)
                .GroupBy(sr => sr.ServerID)
                .ToDictionary(
                    g => g.Key,
                    g => g.FirstOrDefault()?.Role?.RoleName ?? "Unknown"
                );

            ViewBag.UserRoles = userRoleMap;

            ViewBag.UserName = user.Username;
            ViewBag.CreatedServers = user.CreatedServers;
            ViewBag.JoinedServers = joinedServers;

            return View();
        }

        public IActionResult RecentMessages()
        {
            var recent = _context.UserRecentActivity.ToList();
            return View(recent);
        }



    }
}

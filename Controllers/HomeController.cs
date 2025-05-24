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


            ViewBag.UserName = user.Username;
            ViewBag.CreatedServers = user.CreatedServers;
            ViewBag.JoinedServers = joinedServers;

            return View();
        }

    }
}

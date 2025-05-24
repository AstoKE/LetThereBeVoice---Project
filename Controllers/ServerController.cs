using Microsoft.AspNetCore.Mvc;
using LetThereBeVoice.Data;
using LetThereBeVoice.Models;

namespace LetThereBeVoice.Controllers
{
    public class ServerController : Controller
    {
        private readonly AppDbContext _context;

        public ServerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Server/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Server/Create
        [HttpPost]
        public IActionResult Create(string serverName)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var server = new Server
            {
                ServerName = serverName,
                CreatorID = userId.Value,
                CreationDate = DateTime.UtcNow
            };

            _context.Servers.Add(server);
            _context.SaveChanges();

            // Kullanıcı otomatik olarak kendi oluşturduğu sunucuya katılsın
            var userServer = new UserServer
            {
                UserID = userId.Value,
                ServerID = server.ServerID
            };

            _context.UserServer.Add(userServer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: /Server/Join
        public IActionResult Join()
        {
            return View();
        }

        // POST: /Server/Join
        [HttpPost]
        public IActionResult Join(int serverId)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            bool alreadyJoined = _context.UserServer.Any(us => us.UserID == userId && us.ServerID == serverId);
            if (!alreadyJoined)
            {
                _context.UserServer.Add(new UserServer
                {
                    UserID = userId.Value,
                    ServerID = serverId
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

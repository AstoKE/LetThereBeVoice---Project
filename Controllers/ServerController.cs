using Microsoft.AspNetCore.Mvc;
using LetThereBeVoice.Data;
using LetThereBeVoice.Models;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Server;

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
            _context.UserServer.Add(new UserServer
            {
                UserID = userId.Value,
                ServerID = server.ServerID
            });

            // Oluşturucuya Admin rolü ata
            var adminRoleId = _context.Roles.FirstOrDefault(r => r.RoleName == "Admin")?.RoleID ?? 1;

            _context.ServerRoles.Add(new ServerRole
            {
                ServerID = server.ServerID,
                UserID = userId.Value,
                RoleID = adminRoleId
            });

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Join()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var joinedServerIds = _context.UserServer
                .Where(us => us.UserID == userId)
                .Select(us => us.ServerID)
                .ToList();

            var availableServers = _context.Servers
                .Where(s => !joinedServerIds.Contains(s.ServerID))
                .ToList();

            ViewBag.AvailableServers = availableServers;

            return View();
        }



        [HttpPost]
        public IActionResult Join(int serverId)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var existing = _context.UserServer
                .FirstOrDefault(us => us.ServerID == serverId && us.UserID == userId);

            if (existing == null)
            {
                _context.UserServer.Add(new UserServer
                {
                    ServerID = serverId,
                    UserID = userId.Value
                });

                // Sadece Member rolü ata
                var memberRoleId = _context.Roles.FirstOrDefault(r => r.RoleName == "Member")?.RoleID;
                if (memberRoleId == null)
                {
                    TempData["Error"] = "Member role not found.";
                    return RedirectToAction("Join");
                }

                _context.ServerRoles.Add(new ServerRole
                {
                    ServerID = serverId,
                    UserID = userId.Value,
                    RoleID = memberRoleId.Value
                });

                _context.SaveChanges();
            }

            var server = _context.Servers.FirstOrDefault(s => s.ServerID == serverId);
            if (server == null)
            {
                TempData["Error"] = "Server not found.";
                return RedirectToAction("Join");
            }

            return RedirectToAction("Index", "Home");
        }


    }
}

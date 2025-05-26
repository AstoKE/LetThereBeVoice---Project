# LetThereBeVoice 🎙️  
A Discord-inspired ASP.NET Core MVC application for voice & text communication, server and channel management, and user interaction (roles, friendships, stats).

---

## 🛠️ Technologies Used

- **.NET 8**
- **ASP.NET Core MVC**
- **Entity Framework Core (EF Core)** (SQL Server Provider)
- **Bootstrap 5**
- **SQL Server** (LocalDB or any available SQL Server instance)

---

## 📦 Dependencies

Make sure the following NuGet packages are installed:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Session
Or install via Visual Studio NuGet Manager.

⚙️ Local Setup Instructions
1. Clone the repository
bash
Kopyala
Düzenle
git clone https://github.com/yourusername/LetThereBeVoice.git
cd LetThereBeVoice
2. Setup SQL Server (LocalDB)
Make sure you have SQL Server (Express/LocalDB) installed and running. You can check this via:

bash
Kopyala
Düzenle
sqllocaldb start
sqllocaldb info
Or use full SQL Server with correct connection string in appsettings.json.

3. Update the Database
Run EF Core migrations and seed roles:

bash
Kopyala
Düzenle
dotnet ef database update
Then insert the base roles manually if needed:

sql
Kopyala
Düzenle
SET IDENTITY_INSERT Roles ON;
INSERT INTO Roles (RoleID, RoleName) VALUES (1, 'Admin'), (2, 'Member');
SET IDENTITY_INSERT Roles OFF;
4. Launch the Application
bash
Kopyala
Düzenle
dotnet run
Visit https://localhost:7187 or http://localhost:5299

🔐 HTTPS Development Certificates
If needed, trust the dev cert:

bash
Kopyala
Düzenle
dotnet dev-certs https --trust
📋 Features
🔐 User Login/Register

🎙️ Voice Session Simulation

📝 Text Messaging per Channel

📁 Server & Channel Management

👥 Friendship System (requests, list, add)

🛡️ Role Management (Admin/Member)

📊 Stats (via LINQ & SQL Views)

🧠 Trigger-based activity update (EF logic)

📈 Dashboard UI with Bootstrap 5

👤 Admin Functionality
Only users with Admin role on a server can:

Edit/Delete Channels

Manage Roles (future)

Access Admin Panel (future)

🧪 Development Notes
Roles are enforced in views and backend.

LastActivity is updated when a message is sent.

UserRecentActivity SQL View is used for stats display.

Use Developer Tools (F12) to trace live UI data bindings.

📁 Folder Structure
bash
Kopyala
Düzenle
LetThereBeVoice/
│
├── Controllers/           # MVC Controllers
├── Models/                # EF Models
├── Views/                 # Razor Views
├── Data/                  # AppDbContext & Migrations
├── wwwroot/               # Static files (CSS, JS, etc.)
└── Program.cs / Startup.cs
✍️ Contributors
Developer: [Your Name]

University: TED University – CMPE232 Term Project Spring 2025

📄 License
This project is for academic use only.

yaml
Kopyala
Düzenle

---

Eğer bu dosyayı doğrudan `.md` formatında eklemek istersen senin için dosya haline de getirebilirim. Ay

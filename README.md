LetThereBeVoice 
A web-based voice & text communication platform inspired by Discord, built with ASP.NET Core MVC and Entity Framework Core.

---Libraries / Technologies Used

Visual Studio 2022

.NET 8

ASP.NET Core MVC

Entity Framework Core (EF Core) – SQL Server provider

Bootstrap 5

SQL Server LocalDB or full instance

---How to Run the Project (Step-by-Step)

Clone the Repository
(or just use zip file)s

git clone https://github.com/yourusername/LetThereBeVoice.git
cd LetThereBeVoice
Ensure Prerequisites Are Installed

.NET 8 SDK

SQL Server LocalDB

Visual Studio (recommended) or VS Code

---Install Required NuGet Packages

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Session

---Create the Database

dotnet ef database update
Insert Default Roles (Admin, Member)

---sql

SET IDENTITY_INSERT Roles ON;
INSERT INTO Roles (RoleID, RoleName) VALUES (1, 'Admin'), (2, 'Member');
SET IDENTITY_INSERT Roles OFF;
Trust HTTPS Certificate (if needed)


dotnet dev-certs https --trust

---Run the Project

dotnet run
Open:

https://localhost:7187

or http://localhost:5299

--Features

User Registration & Login

Server & Channel Management

Text Messaging per Channel

Simulated Voice Session Panel

Role Assignment (Admin / Member)

Friendship System (Add, Accept, View)

Real-Time Stats with LINQ & SQL Views

LastActivity tracking via trigger logic

Fully responsive UI with Bootstrap

---Group Info:

Emir Ağaoğlu(1009328300)
Şükrü Enes Tuğaç(12133216098)
Barkın Bacalan(13964010824)
Ceren Kısacık(18913012072)

University: TED University
Course: CMPE232 – Spring 2025 Term Project
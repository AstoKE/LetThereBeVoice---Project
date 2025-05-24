using LetThereBeVoice.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession(); 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


app.MapDefaultControllerRoute();

app.UseStaticFiles();
app.UseRouting();

app.UseSession(); 

app.MapDefaultControllerRoute();
app.Run();

using Microsoft.EntityFrameworkCore;
using LibraryWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. ИСПРАВЛЕННЫЙ ПУТЬ К БАЗЕ:
// Берем путь из переменных Railway, если её нет — используем локальную базу
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=library.db";

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddRazorPages();

// 2. ДОБАВЛЯЕМ ПОДДЕРЖКУ КОНТРОЛЛЕРОВ (для скачивания базы):
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    db.Database.EnsureCreated();
    if (!db.Books.Any())
    {
        db.Books.Add(new Book { Title = "Мастер и Маргарита", Author = "Михаил Булгаков" });
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 3. ВКЛЮЧАЕМ МАРШРУТЫ ДЛЯ КОНТРОЛЛЕРОВ:
app.MapControllers(); 
app.MapRazorPages();

app.Run();

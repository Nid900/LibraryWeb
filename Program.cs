using Microsoft.EntityFrameworkCore;
using LibraryWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Подключаем базу данных SQLite
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite("Data Source=library.db"));

builder.Services.AddRazorPages();

var app = builder.Build();

// Код для автоматического создания базы и тестовой книги
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
app.MapRazorPages();

app.Run();
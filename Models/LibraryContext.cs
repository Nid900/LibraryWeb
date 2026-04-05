using Microsoft.EntityFrameworkCore;

namespace
LibraryWeb.Models;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Issue> Issues { get; set; }
}
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryWeb.Models;

namespace LibraryWeb.Pages;

public class IndexModel : PageModel
{
    private readonly LibraryContext _db;
    public List<Book> Books { get; set; } = new();

    public IndexModel(LibraryContext db) => _db = db;

    public void OnGet()
    {
        Books = _db.Books.ToList();
    }
}
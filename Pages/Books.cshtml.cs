using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Models;

namespace LibraryWeb.Pages
{
    public class BooksModel : PageModel
    {
        private readonly LibraryContext _context;

        public BooksModel(LibraryContext context)
        {
            _context = context;
        }

        public List<Book> Books { get; set; } = new();

        public async Task OnGetAsync()
        {
            Books = await _context.Books.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string title, string author)
        {
            if (!string.IsNullOrEmpty(title))
            {
                _context.Books.Add(new Book { Title = title, Author = author, IsIssued = false });
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var relatedIssues = _context.Issues.Where(i => i.BookId == id).ToList();
                _context.Issues.RemoveRange(relatedIssues);
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
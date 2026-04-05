using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Models;

namespace LibraryWeb.Pages
{
    public class IssuesModel : PageModel
    {
        private readonly LibraryContext _context;

        public IssuesModel(LibraryContext context)
        {
            _context = context;
        }

        public List<Issue> Issues { get; set; } = new();
        public List<Book> AvailableBooks { get; set; } = new();
        public List<Reader> Readers { get; set; } = new();

        public async Task OnGetAsync()
        {
            Issues = await _context.Issues
                .Include(i => i.Book)
                .Include(i => i.Reader)
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();

            AvailableBooks = await _context.Books.Where(b => !b.IsIssued).ToListAsync();
            Readers = await _context.Readers.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int bookId, int readerId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.IsIssued = true;
                _context.Issues.Add(new Issue 
                { 
                    BookId = bookId, 
                    ReaderId = readerId, 
                    IssueDate = DateTime.Now 
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostReturnAsync(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue != null)
            {
                issue.ReturnDate = DateTime.Now;
                var book = await _context.Books.FindAsync(issue.BookId);
                if (book != null) book.IsIssued = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
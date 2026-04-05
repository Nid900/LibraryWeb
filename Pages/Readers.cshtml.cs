using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Models;

namespace LibraryWeb.Pages
{
    public class ReadersModel : PageModel
    {
        private readonly LibraryContext _context;

        public ReadersModel(LibraryContext context)
        {
            _context = context;
        }

        public List<Reader> Readers { get; set; } = new();

        public async Task OnGetAsync()
        {
            Readers = await _context.Readers.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string fullName, string phone)
        {
            if (!string.IsNullOrEmpty(fullName))
            {
                _context.Readers.Add(new Reader { FullName = fullName, Phone = phone });
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader != null)
            {
                var hisIssues = _context.Issues.Where(i => i.ReaderId == id).ToList();
                foreach (var issue in hisIssues)
                {
                    var book = await _context.Books.FindAsync(issue.BookId);
                    if (book != null) book.IsIssued = false;
                }
                _context.Issues.RemoveRange(hisIssues);
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
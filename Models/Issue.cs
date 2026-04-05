using System;

namespace LibraryWeb.Models;

public class Issue
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int ReaderId { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.Now;
    public DateTime? ReturnDate { get; set; }

    // Связи с другими таблицами
    public Book? Book { get; set; }
    public Reader? Reader { get; set; }
}
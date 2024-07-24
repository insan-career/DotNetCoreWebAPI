using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Services;

public class BookService
{
    private static readonly List<Book> Books = new();

    public List<Book> GetAllBooks()
    {
        return Books.ToList(); // Return a copy to avoid modifying original list
    }

    public Book? GetBookById(int id)
    {
        var book =  Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            throw new ArgumentException($"Book with ID {id} not found.");
        }
        return book;
    }

    public Book AddBook(Book book)
    {
        if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
        {
            throw new ArgumentException("Title and Author are required fields.");
        }

        book.Id = Books.Count + 1; // Generate a unique ID
        Books.Add(book);
        return book;
    }

    public Book UpdateBook(int id, Book? book)
    {
        var existingBook = GetBookById(id);
        if (existingBook == null)
        {
            throw new ArgumentException($"Book with ID {id} not found.");
        }

        if (book == null || string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
        {
            throw new ArgumentException("Title and Author are required fields.");
        }

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.PublishedDate = book.PublishedDate;
        return existingBook;
    }

    public void DeleteBook(int id)
    {
        var book = GetBookById(id);
        if (book == null)
        {
            throw new ArgumentException($"Book with ID {id} not found.");
        }

        Books.Remove(book);
    }
}
using Microsoft.AspNetCore.Mvc;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Services;

namespace DotNetCoreWebAPI.Controllers;

public class BooksController: ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    [Route("api/[controller]/GetBooks")]
    public IActionResult GetBooks()
    {
        var books = _bookService.GetAllBooks();
        return Ok(books);
    }

    [HttpGet]
    [Route("api/[controller]/GetBook/{id}", Name = "GetBook")]
    public IActionResult GetBook(int id)
    {
        try
        {
            var book = _bookService.GetBookById(id);
            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [Route("api/[controller]/AddBook")]
    public IActionResult AddBook([FromBody] Book? book)
    {
        try
        {
            if (book == null)
            {
                return BadRequest("Please provide title, author and published date in the request body");
            }
                
            if (book.Id != 0)
            {
                return BadRequest("Id should not be provided in the request body.");
            }
            var newBook = _bookService.AddBook(book);
            return CreatedAtRoute("GetBook", new { id = newBook.Id }, newBook);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut]
    [Route("api/[controller]/UpdateBook/{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book? book)
    {
        try
        {
            if (book == null)
            {
                return BadRequest("Please provide title, author and published date in the request body");
            }
            
            if (book.Id != id)
            {
                return BadRequest("Id in the request body does not match parameter id.");
            }
            _bookService.UpdateBook(id, book);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("api/[controller]/DeleteBook/{id}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
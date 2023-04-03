using BooksDemo.Models.Books;
using BooksDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;

    public BooksController(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }

    // GET: api/<BooksController>
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    // GET api/<BooksController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return Ok(book);
    }

    // POST api/<BooksController>
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookRequest model)
    {
        await _authorService.GetAuthorByIdAsync(model.AuthorId);

        var book = await _bookService.CreateBookAsync(model);

        return book != 0 ?
            Ok("The book was successfully added to the database") :
            BadRequest("Something wrong with request");
    }

    // PUT api/<BooksController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookRequest model)
    {
        await _authorService.GetAuthorByIdAsync(model.AuthorId);
        await _bookService.UpdateBookAsync(id, model);
        return Ok("The book was successfully updated in the database");
    }

    // DELETE api/<BooksController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return Ok("The book was successfully deleted in the database");
    }
}
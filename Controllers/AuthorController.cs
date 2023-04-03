using BooksDemo.Models.Authors;
using BooksDemo.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // GET: api/<AuthorController>
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    // GET api/<AuthorController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id, bool includeBooks = false)
    {
        var author = await _authorService.GetAuthorByIdAsync(id, includeBooks);
        return Ok(author);
    }

    // POST api/<AuthorController>
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorRequest model)
    {
        var authorId = await _authorService.CreateAuthor(model);

        return authorId != 0 ?
            Ok(new { message = $"Author was successfully created in database with the id {authorId}" }) :
            BadRequest("Something wrong with request");
    }

    // PUT api/<AuthorController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorRequest model)
    {
        await _authorService.UpdateAuthor(id, model);
        return Ok(new { message = "Author was successfully updated in database" });

    }

    // DELETE api/<AuthorController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorService.DeleteAuthor(id);
        return Ok(new { message = "Author was successfully deleted in database" });

    }
}
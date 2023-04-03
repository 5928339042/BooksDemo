using AutoMapper;
using BooksDemo.Data;
using BooksDemo.Entities;
using BooksDemo.Helpers;
using BooksDemo.Models.Authors;
using Microsoft.EntityFrameworkCore;

namespace BooksDemo.Services;

public interface IAuthorService
{
    /// <summary>
    /// Get all authors in database. Set includeBooks to true if you want to include all books made by the author.
    /// </summary>
    /// <param name="includeBooks">Optional parameter to include books</param>
    /// <returns>All authors in database</returns>
    Task<IEnumerable<Author>> GetAllAuthorsAsync(bool includeBooks = false);

    /// <summary>
    /// Get a single author by Id and include books if requested by the includeBooks boolean.
    /// </summary>
    /// <param name="id">Id of Author</param>
    /// <param name="includeBooks">Optional parameter to include books</param>
    /// <returns>A single author</returns>
    Task<Author> GetAuthorByIdAsync(int id, bool includeBooks = false);

    /// <summary>
    /// Create a new author in the database
    /// </summary>
    /// <param name="model">Create Author request model</param>
    Task<int> CreateAuthorAsync(CreateAuthorRequest model);

    /// <summary>
    /// Update an author in the database if the author already exists.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    Task UpdateAuthorAsync(int id, UpdateAuthorRequest model);

    /// <summary>
    /// Delete a single author in the database. Will delete the author if the author exists in the database.
    /// Cascading is enabled and will delete the authors books from the database at the same time. Use with caution.
    /// </summary>
    /// <param name="id">Id of the author to delete</param>
    Task DeleteAuthorAsync(int id);
}

public class AuthorService : IAuthorService
{
    private readonly LibraryContext _dbContext;
    private readonly IMapper _mapper;

    public AuthorService(LibraryContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> CreateAuthorAsync(CreateAuthorRequest model)
    {
        // Validate new author
        if (await _dbContext.Authors.AnyAsync(x => x.Name == model.Name))
            throw new RepositoryException($"An author with the name {model.Name} already exists.");

        // Map model to new author object
        var author = _mapper.Map<Author>(model);

        // Save Author
        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync().ConfigureAwait(true);

        return author?.Id ?? 0;
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await GetAuthorByIdAsync(id);

        _dbContext.Authors.Remove(author); // Delete the author and books (Cascading is enabled)
        await _dbContext.SaveChangesAsync().ConfigureAwait(true);
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(bool includeBooks = false)
    {
        if (includeBooks)
        {
            // Get all authors and their books
            return await _dbContext.Authors
                .Include(b => b.Books)
                .ToListAsync().ConfigureAwait(true);
        }

        // Get all authors without including the books
        return await _dbContext.Authors
            .ToListAsync().ConfigureAwait(true);
    }

    public async Task<Author> GetAuthorByIdAsync(int id, bool includeBooks = false)
    {
        if (includeBooks)
        {
            var author = await _dbContext.Authors
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(b => b.Books)
                .FirstOrDefaultAsync().ConfigureAwait(true);

            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }

            return author;
        }
        else
        {
            var author = await _dbContext.Authors
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync().ConfigureAwait(true);

            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }

            return author;
        }
    }

    public async Task UpdateAuthorAsync(int id, UpdateAuthorRequest model)
    {
        var author = await GetAuthorByIdAsync(id);

        // Validation
        if (model.Name != author.Name && await _dbContext.Authors.AnyAsync(x => x.Name == model.Name))
            throw new RepositoryException($"An author with the name {model.Name} already exists.");

        // copy model to author and save
        _mapper.Map(model, author);
        _dbContext.Authors.Update(author);
        await _dbContext.SaveChangesAsync();

    }
}
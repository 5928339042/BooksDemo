﻿using AutoMapper;
using BooksDemo.Data;
using BooksDemo.Entities;
using BooksDemo.Helpers;
using BooksDemo.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace BooksDemo.Services;

public interface IBookService
{
    /// <summary>
    /// Get all book in database.
    /// </summary>
    /// <returns>All books in database</returns>
    Task<IEnumerable<Book>> GetAllBooksAsync();

    /// <summary>
    /// Get a single book by Id
    /// </summary>
    /// <param name="id">Id of book</param>
    /// <returns>A single book</returns>
    Task<Book> GetBookByIdAsync(int id);

    /// <summary>
    /// Create a new book in the database
    /// </summary>
    /// <param name="model">Create book request model</param>
    Task<int> CreateBookAsync(CreateBookRequest model);

    /// <summary>
    /// Update a book in the database if the book already exists.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    Task UpdateBookAsync(int id, UpdateBookRequest model);

    /// <summary>
    /// Delete a single book in the database. Will delete the book if the book exists in the database.
    /// </summary>
    /// <param name="id">Id of the book to delete</param>
    Task DeleteBookAsync(int id);
}

public class BookService : IBookService
{
    private readonly LibraryContext _dbContext;
    private readonly IMapper _mapper;

    public BookService(LibraryContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> CreateBookAsync(CreateBookRequest model)
    {
        // Validate new book
        if (await _dbContext.Books.AnyAsync(x => x.ISBN13 == model.ISBN13))
            throw new RepositoryException($"A book with the ISBN {model.ISBN13} already exist in the database");

        // Map model to new book object
        var book = _mapper.Map<Book>(model);

        // Save book in database
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync().ConfigureAwait(true);

        return book.Id;
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await GetBookByIdAsync(id);

        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync().ConfigureAwait(true);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _dbContext.Books
            .ToListAsync()
            .ConfigureAwait(true);
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        var book = await _dbContext.Books
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync().ConfigureAwait(true);

        if (book == null)
        {
            throw new KeyNotFoundException("Book was not found in database");
        }

        return book;
    }

    public async Task UpdateBookAsync(int id, UpdateBookRequest model)
    {
        var book = await GetBookByIdAsync(id);

        // Validate the book
        if (model.ISBN13 != book.ISBN13 && await _dbContext.Books.AnyAsync(x => x.ISBN13 == model.ISBN13))
            throw new RepositoryException($"A book with the ISBN number {model.ISBN13} already exist in the database.");

        // Copy model data to book object and save it in the database
        _mapper.Map(model, book);
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync().ConfigureAwait(true);
    }
}
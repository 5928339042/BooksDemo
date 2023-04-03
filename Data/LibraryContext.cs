using BooksDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksDemo.Data;

public class LibraryContext : DbContext
{
    private readonly IConfiguration _configuration;

    public LibraryContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(b => b.Books)
            .WithOne(a => a.Author)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
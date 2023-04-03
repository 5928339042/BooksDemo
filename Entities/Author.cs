namespace BooksDemo.Entities;

public class Author : Base
{
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public IEnumerable<Book>? Books { get; set; }
}
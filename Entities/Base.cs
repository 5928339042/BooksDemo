namespace BooksDemo.Entities;

public class Base
{
    public int Id { get; set; } = 0;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}
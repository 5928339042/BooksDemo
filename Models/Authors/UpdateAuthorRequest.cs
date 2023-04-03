namespace BooksDemo.Models.Authors;

public class UpdateAuthorRequest
{
    public string Name { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public DateTime Updated { get; set; } = DateTime.UtcNow;
}
using System.ComponentModel.DataAnnotations;

namespace BooksDemo.Models.Authors;

public class CreateAuthorRequest
{
    [Required]
    public string Name { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;
}
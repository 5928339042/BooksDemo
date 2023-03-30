using System.ComponentModel.DataAnnotations;
using BooksDemo.Entities;

namespace BooksDemo.Models.Authors
{
    public class CreateAuthorRequest
    {
        [Required]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}

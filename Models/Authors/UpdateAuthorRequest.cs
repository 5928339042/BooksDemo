using System.ComponentModel.DataAnnotations;
using BooksDemo.Entities;

namespace BooksDemo.Models.Authors
{
    public class UpdateAuthorRequest
    {
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}

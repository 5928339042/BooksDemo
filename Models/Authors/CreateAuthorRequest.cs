using System.ComponentModel.DataAnnotations;
using BooksDemo.Entities;

namespace BooksDemo.Models.Authors
{
    public class CreateAuthorRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}

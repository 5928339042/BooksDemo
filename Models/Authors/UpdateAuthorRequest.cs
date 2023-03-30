using System.ComponentModel.DataAnnotations;
using BooksDemo.Entities;

namespace BooksDemo.Models.Authors
{
    public class UpdateAuthorRequest
    {
        public string Name { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public DateTime Updated { get; set; } = DateTime.Now;
    }
}

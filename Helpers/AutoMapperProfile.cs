using AutoMapper;
using BooksDemo.Entities;
using BooksDemo.Models.Authors;
using BooksDemo.Models.Books;

namespace BooksDemo.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateAuthorRequest => Author
            CreateMap<CreateAuthorRequest, Author>()
                .AfterMap((_, author) =>
                {
                    SetUtcKind(author);
                });

            // UpdateAuthorRequest => Author
            CreateMap<UpdateAuthorRequest, Author>()
                .AfterMap((_, author) =>
                {
                    SetUtcKind(author);
                })
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // let's ignore null and empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

            // CreateBookRequest => Book
            CreateMap<CreateBookRequest, Book>()
                .AfterMap((_, book) => book.Created = DateTime.SpecifyKind(book.Created, DateTimeKind.Utc));

            // UpdateBookRequest => Book
            CreateMap<UpdateBookRequest, Book>()
                .AfterMap((_, book) => book.Updated = DateTime.SpecifyKind(book.Updated, DateTimeKind.Utc));
        }

        private void SetUtcKind(Author author)
        {
            if (author.BirthDate.HasValue)
            {
                author.BirthDate = DateTime.SpecifyKind(author.BirthDate.Value, DateTimeKind.Utc);
            }
            author.Created = DateTime.SpecifyKind(author.Created, DateTimeKind.Utc);
        }
    }
}

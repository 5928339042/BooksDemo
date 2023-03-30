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
            CreateMap<CreateAuthorRequest, Author>();

            // UpdateAuthorRequest => Author
            CreateMap<UpdateAuthorRequest, Author>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // let's ignore null and empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // Ignore null gender
                        if (x.DestinationMember.Name == "Gender" && src.Gender == null) return false;

                        return true;
                    }
                  ));

            // CreateBookRequest => Book
            CreateMap<CreateBookRequest, Book>();

            // UpdateBookRequest => Book
            CreateMap<UpdateBookRequest, Book>();
        }
    }
}

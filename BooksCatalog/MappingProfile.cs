using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksCatalog
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<AuthorForCreationDto, Author>();
            CreateMap<Book, BookDto>();
            CreateMap<Book, AuthorBooksDto>();
        }
    }
}

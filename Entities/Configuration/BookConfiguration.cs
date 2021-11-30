using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            Author author = new Author
            {
                Id = 1,
                Surname = "Лукин"
            };
            builder.HasData
            (

                new Book
                {
                    Name = "Катали мы ваше солнце",
                    Year = 2009,
                    Authors = new List<Author> { author }
                },
                new Book
                {
                    Name = "Зов Ктулху",
                    Year = 1928
                }
            );
        }
    }
}

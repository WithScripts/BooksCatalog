using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData
            (
                new Author
                {
                    Surname = "Лукин"
                },
                new Author
                {
                    Surname = "Лавкрафт",
                },
                new Author
                {
                    Surname = "Ноунейм"
                }
            ); ;
        }
    }
}

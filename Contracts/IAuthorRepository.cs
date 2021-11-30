using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAuthorRepository
    {
        IList<Author> GetAuthors();
        void CreateAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
    }
}

using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IList<Author> GetAuthors() => 
            RepositoryContext.Authors.Include(a => a.Books).ToList();
        public void CreateAuthor(Author author) => Create(author);
        public void DeleteAuthor(Author author) => Delete(author);
        public void UpdateAuthor(Author author) => Update(author);
    }
}

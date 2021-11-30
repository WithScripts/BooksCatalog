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
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IList<Book> GetBooks() =>
            RepositoryContext.Books.Include(a => a.Authors).ToList();
        public void CreateBook(Book book) => Create(book);
        public void DeleteBook(Book book) => Delete(book);
        public void UpdateBook(Book book) => Update(book);
    }
}

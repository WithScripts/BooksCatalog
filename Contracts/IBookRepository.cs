using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IBookRepository
    {
        IList<Book> GetBooks();
        void CreateBook(Book book);
        void DeleteBook(Book book);
        void UpdateBook(Book book);
    }
}

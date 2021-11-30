using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        void Save();
    }
}

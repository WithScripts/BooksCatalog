using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AddBookToAuthorDto
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
    }
}

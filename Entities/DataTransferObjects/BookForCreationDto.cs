using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class BookForCreationDto
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
    }
}

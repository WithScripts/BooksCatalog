using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
        public Book()
        {
            Authors = new List<Author>();
        }
    }
}

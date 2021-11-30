using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Book> Books{ get; set; }
        public Author()
        {
            Books = new List<Book>();
        }
    }
}

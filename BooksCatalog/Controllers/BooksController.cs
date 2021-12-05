using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IEnumerable<Book> _books;

        public BooksController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _books = _repository.Book.GetBooks();
        }
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookForCreationDto book)
        {
            if(book == null) return BadRequest("field \"Name\" or \"Year\" are empty");
            Book bookEntity = _mapper.Map<Book>(book);
            Author author = _repository.Author.GetAuthors().FirstOrDefault(c => c.Id == book.AuthorId);
            if (author != null) bookEntity.Authors.Add(author);
            _repository.Book.CreateBook(bookEntity);
            _repository.Save();
            return CreatedAtRoute("BookById", new { id = bookEntity.Id }, bookEntity);
        }
        [HttpGet("{id}", Name = "BookById")]
        public IActionResult GetBookById(int id)
        {
            Book book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return BadRequest("Book not exist");
            BookDto bookDto = _mapper.Map<BookDto>(book);
            bookDto.Authors = _repository.Book.GetAuthorsInString(book);
            return Ok(bookDto);
        }
        [HttpDelete("{id}", Name = "DeleteById")]
        public IActionResult DeleteBookById(int id)
        {
            Book book = _books.FirstOrDefault(c => c.Id == id);
            if (book == null) return BadRequest("Book not exist");
            _repository.Book.DeleteBook(book);
            _repository.Save();
            return Ok("Book deleted");
        }
    }
}

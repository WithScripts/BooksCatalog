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
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IEnumerable<Book> _books;

        public BooksController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _books = _repository.Book.GetBooks();
        }
        [HttpGet(Name = "GetBooks")]
        public IActionResult GetBooks()
        {
            try
            {
                return Ok(_books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetBooks)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookForCreationDto book)
        {
            if (book == null)
            {
                _logger.LogError("Is null");
                return BadRequest("Null");
            }
            Book bookEntity = _mapper.Map<Book>(book);
            IList<Author> authors = _repository.Author.GetAuthors();
            Author author = authors.FirstOrDefault(c => c.Id == book.AuthorId);
            bookEntity.Authors.Add(author);
            _repository.Book.CreateBook(bookEntity);
            _repository.Save();
            string authros = "";
            foreach (var obj in bookEntity.Authors)
            {
                authros += $"Id: {obj.Id}, Surname: {obj.Surname}";
            }
            var bookDto = _mapper.Map<BookForShowDto>(bookEntity);
            bookDto.Authros = authros;
            return CreatedAtRoute("BookById", new { id = bookDto.Id }, bookDto);
        }
        [HttpGet("{id}", Name = "BookById")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                Book book = _books.First(b => b.Id == id);
                string authros = "";
                foreach(var obj in book.Authors)
                {
                    authros += $"Id: {obj.Id}, Surname: {obj.Surname}";
                }
                var bookDto = _mapper.Map<BookForShowDto>(book);
                bookDto.Authros = authros;
                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetBookById)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}", Name = "DeleteById")]
        public IActionResult DeleteBookById(int id)
        {
            try
            {
                Book book = _books.First(c => c.Id == id);
                _repository.Book.DeleteBook(book);
                _repository.Save();
                return Ok("Book deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteBookById)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

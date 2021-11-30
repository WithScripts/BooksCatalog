using AutoMapper;
using Contracts;
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
    public class AuthorController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IList<Author> _authors;

        public AuthorController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _authors = _repository.Author.GetAuthors();
        }
        [HttpGet]
        public IActionResult GetAuthors()
        {
            try
            {
                var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(_authors);
                return Ok(authorsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAuthors)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorForCreationDto author)
        {
            if (author == null)
            {
                _logger.LogError("Is null");
                return BadRequest("Null");
            }
            Author authorEntity = _mapper.Map<Author>(author);
            _repository.Author.CreateAuthor(authorEntity);
            _repository.Save();
            return CreatedAtRoute("GetBooksByAuthorsId", new { authorId = authorEntity.Id }, authorEntity);
        }
        [HttpGet("{authorId}", Name = "GetBooksByAuthorsId")]
        public IActionResult GetBooksByAuthorsId(int authorId)
        {
            try
            {
                var books = _authors.First(a => a.Id == authorId).Books;
                return books.Count != 0 ? Ok(books) : Ok("The author hasn't books");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetBooksByAuthorsId)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            Author author = _authors.First(c => c.Id == id);
            if(author == null)
            {
                _logger.LogError("Is null");
                return BadRequest("Null");
            }
            _repository.Author.DeleteAuthor(author);
            _repository.Save();
            return Ok("The author deleted");
        }
        [HttpPut]
        public IActionResult AddBookToAuthor([FromBody]AuthorBookDto authorBook)
        {
            if(authorBook == null)
            {
                _logger.LogError("Is null");
                return BadRequest("Null");
            }
            Author author = _authors.First(a => a.Id == authorBook.AuthorId);
            IList<Book> books = _repository.Book.GetBooks();
            Book book = books.First(b => b.Id == authorBook.BookId);
            author.Books.Add(book);
            _repository.Author.UpdateAuthor(author);
            _repository.Save();
            return CreatedAtRoute("GetBooksByAuthorsId", new { authorId = author.Id }, author);
        }
    }
}

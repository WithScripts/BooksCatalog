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
        private readonly IMapper _mapper;
        private readonly IList<Author> _authors;

        public AuthorController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _authors = _repository.Author.GetAuthors();
        }
        [HttpGet]
        public IActionResult GetAuthors()
        {
            IEnumerable<AuthorDto> authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(_authors);
            return authorsDto != null ? Ok(authorsDto) : Ok("No authors");
        }
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorForCreationDto author)
        {
            if (author == null) return BadRequest("field \"Surname\" is empty");
            Author authorEntity = _mapper.Map<Author>(author);
            _repository.Author.CreateAuthor(authorEntity);
            _repository.Save();
            AuthorDto authorDto = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetBooksByAuthorsId", new { authorId = authorDto.Id }, authorDto);
        }
        [HttpGet("{authorId}", Name = "GetBooksByAuthorsId")]
        public IActionResult GetBooksByAuthorsId(int authorId)
        {
            Author author = _authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null) return BadRequest("The author dosen't exist");
            ICollection<Book> books = author.Books;
            ICollection<AuthorBooksDto> booksDto = _mapper.Map<ICollection<AuthorBooksDto>>(books);
            return booksDto.Count != 0 ? Ok(booksDto) : Ok("The author hasn't books");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            Author author = _authors.FirstOrDefault(c => c.Id == id);
            if (author == null) return BadRequest("The author doesn't exist");
            _repository.Author.DeleteAuthor(author);
            _repository.Save();
            return Ok("The author deleted");
        }
        [HttpPut]
        public IActionResult AddBookToAuthor([FromBody]AddBookToAuthorDto authorBook)
        {
            Author author = _authors.FirstOrDefault(a => a.Id == authorBook.AuthorId);
            Book book = _repository.Book.GetBooks().FirstOrDefault(b => b.Id == authorBook.BookId);
            if (author == null) return BadRequest("The author dosen't exist");
            else if (book == null) return BadRequest("The book dosen't exist");
            author.Books.Add(book);
            _repository.Author.UpdateAuthor(author);
            _repository.Save();
            return CreatedAtRoute("GetBooksByAuthorsId", new { authorId = author.Id }, author);
        }
    }
}

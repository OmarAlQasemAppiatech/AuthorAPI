using Author_API.Dtos;
using Author_API.Entities;
using Author_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Controllers
{
    [ApiController]
    [Route("Authors")]
    public class AuthorController : Controller
    {
        private readonly IAuthorsRepository _repository;

        public AuthorController(IAuthorsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<AuthorDto> GetAuthors()
        {
            var Authors = _repository.GetAuthors().Select(Author => Author.AsDto());
            return Authors;
        }

        [HttpGet("{AuthorId}")]
        public AuthorDto GetAuthorById(int AuthorId)
        {
            var Author = _repository.GetAuthor(AuthorId).AsDto();
            return Author;
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(CreateAuthorDto AuthorDto)
        {
            Author Author = new()
            {
                AuthorId=new(),
                Name = AuthorDto.Name,
                Email = AuthorDto.Email,
                DateOfBirth = AuthorDto.DateOfBirth
            };
            _repository.CreateAuthor(Author);
            return CreatedAtAction(nameof(GetAuthorById), new { AuthorId = Author.AuthorId }, Author.AsDto());
        }

        [HttpPut("{AuthorId}")]
        public ActionResult UpdateAuthor(int AuthorId, UpdateAuthorDto AuthorDto)
        {
            var exsistingAuthor = _repository.GetAuthor(AuthorId);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }
            exsistingAuthor.Name = AuthorDto.Name;
            exsistingAuthor.Email = AuthorDto.Email;
            exsistingAuthor.DateOfBirth = AuthorDto.DateOfBirth;
            _repository.UpdateAuthor(exsistingAuthor);
            return NoContent();
        }

        [HttpDelete("{AuthorId}")]
        public ActionResult DeleteAuthor(int AuthorId)
        {
            var exsistingAuthor = _repository.GetAuthor(AuthorId);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }

            _repository.DeleteAuthor(exsistingAuthor);
            return NoContent();
        }
    }
}

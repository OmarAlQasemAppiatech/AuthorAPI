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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorsRepository _repository;

        public AuthorController(IAuthorsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<AuthorResource> GetAuthors()
        {
            var Authors = _repository.GetAuthors().Select(Author => Author.AsResource());
            return Authors;
        }

        [HttpGet("{AuthorId}")]
        public AuthorResource GetAuthorById(int AuthorId)
        {
            var Author = _repository.GetAuthor(AuthorId).AsResource();
            return Author;
        }

        [HttpPost]
        public ActionResult<AuthorResource> CreateAuthor(AuthorModel Model)
        {
            Author Author = new()
            {
                AuthorId=new(),
                Name = Model.Name,
                Email = Model.Email,
                DateOfBirth = Model.DateOfBirth
            };
            _repository.CreateAuthor(Author);
            return CreatedAtAction(nameof(GetAuthorById), new { AuthorId = Author.AuthorId }, Author.AsResource());
        }

        [HttpPut("{AuthorId}")]
        public ActionResult UpdateAuthor(int AuthorId, AuthorModel Model)
        {
            var exsistingAuthor = _repository.GetAuthor(AuthorId);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }
            exsistingAuthor.Name = Model.Name;
            exsistingAuthor.Email = Model.Email;
            exsistingAuthor.DateOfBirth = Model.DateOfBirth;
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

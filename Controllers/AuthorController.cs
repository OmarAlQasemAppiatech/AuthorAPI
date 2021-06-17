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

        [HttpGet("{Id}")]
        public AuthorResource GetAuthorById(int Id)
        {
            var Author = _repository.GetAuthor(Id).AsResource();
            return Author;
        }

        [HttpPost]
        public ActionResult<AuthorResource> CreateAuthor(AuthorModel Model)
        {
            if (Model.Email == "" && Model.PhoneNumber == "")
            {
                return BadRequest();
            }
            
            Author Author = new()
            {
                Id=new(),
                Name = Model.Name,
                Email = Model.Email,
                DateOfBirth = Model.DateOfBirth,
                PhoneNumber=Model.PhoneNumber,
        };
            _repository.CreateAuthor(Author);
            return CreatedAtAction(nameof(GetAuthorById), new { Id = Author.Id }, Author.AsResource());
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateAuthor(int Id, AuthorModel Model)
        {
            var exsistingAuthor = _repository.GetAuthor(Id);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }
            exsistingAuthor.Name = Model.Name;
            exsistingAuthor.Email = Model.Email;
            exsistingAuthor.DateOfBirth = Model.DateOfBirth;
            exsistingAuthor.PhoneNumber = Model.PhoneNumber;

            _repository.UpdateAuthor(exsistingAuthor);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteAuthor(int Id)
        {
            var exsistingAuthor = _repository.GetAuthor(Id);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }

            _repository.DeleteAuthor(exsistingAuthor);
            return NoContent();
        }
    }
}

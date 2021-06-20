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
        public async Task <IEnumerable<AuthorResource>> GetAuthorsAsync()
        {
            var Authors = (await _repository.GetAuthorsAsync()).Select(Author => Author.AsResource());
            return Authors;
        }

        [HttpGet("{Id}")]
        public async Task <AuthorResource> GetAuthorByIdAsync(int Id)
        {
            var Author = await _repository.GetAuthorAsync(Id);
            return Author.AsResource();
        }

        [HttpPost]
        public async Task <ActionResult<AuthorResource>> CreateAuthorAsync(AuthorModel Model)
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
             await _repository.CreateAuthorAsync(Author);
            return CreatedAtAction(nameof(GetAuthorByIdAsync), new { Id = Author.Id }, Author.AsResource());
        }

        [HttpPut("{Id}")]
        public async Task <ActionResult> UpdateAuthorAsync(int Id, AuthorModel Model)
        {
            var exsistingAuthor = await _repository.GetAuthorAsync(Id);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }
            exsistingAuthor.Name = Model.Name;
            exsistingAuthor.Email = Model.Email;
            exsistingAuthor.DateOfBirth = Model.DateOfBirth;
            exsistingAuthor.PhoneNumber = Model.PhoneNumber;

            await _repository.UpdateAuthorAsync(exsistingAuthor);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task <ActionResult> DeleteAuthorAsync(int Id)
        {
            var exsistingAuthor = await _repository.GetAuthorAsync(Id);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }

             await _repository.DeleteAuthorAsync(exsistingAuthor);
            return NoContent();
        }
    }
}

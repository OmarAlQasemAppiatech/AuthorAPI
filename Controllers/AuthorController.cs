﻿using Author_API.Dtos;
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
        public async Task<ActionResult<List<AuthorResource>>> GetAsync()
        {
            var Authors = (await _repository.GetAsync()).Select(Author => Author.AsResource());
            return Ok(Authors);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AuthorResource>> GetByIdAsync(int Id)
        {
            var Author = await _repository.GetByIdAsync(Id);

            if (Author is null)
            {
                return NotFound("There is No Users With Such Id");
            }

            return Ok(Author.AsResource());
        }

        [HttpPost]
        public async Task <ActionResult<AuthorResource>> CreateAsync(AuthorModel Model)
        {
            if (Validate(Model))
            {
                Author Author = new()
                {
                    Id = new(),
                    Name = Model.Name,
                    Email = Model.Email,
                    DateOfBirth = Model.DateOfBirth,
                    PhoneNumber = Model.PhoneNumber,
                };
                await _repository.CreateAsync(Author);
                return CreatedAtAction(nameof(GetByIdAsync), new { Id = Author.Id }, Author.AsResource());
            }
            return BadRequest("Either Email Or Phone Number Must Be Provided");
        }

        [HttpPut("{Id}")]
        public async Task <ActionResult> UpdateAsync(int Id, AuthorModel Model)
        {
            var exsistingAuthor = await _repository.GetByIdAsync(Id);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }

            if (Validate(Model))
            {

                exsistingAuthor.Name = Model.Name;
                exsistingAuthor.Email = Model.Email;
                exsistingAuthor.DateOfBirth = Model.DateOfBirth;
                exsistingAuthor.PhoneNumber = Model.PhoneNumber;

                await _repository.UpdateAsync(exsistingAuthor);
                return NoContent();
            }
            return BadRequest("Either Email Or Phone Number Must Be Provided");
        }

        [HttpDelete("{Id}")]
        public async Task <ActionResult> DeleteAsync(int Id)
        {
            var exsistingAuthor = await _repository.GetByIdAsync(Id);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }

             await _repository.DeleteAsync(exsistingAuthor);
            return NoContent();
        }
        private bool Validate(AuthorModel Model)
        {
            return !(String.IsNullOrEmpty(Model.Email) && String.IsNullOrEmpty(Model.PhoneNumber));
        }
    }
}

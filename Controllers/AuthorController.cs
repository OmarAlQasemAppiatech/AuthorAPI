using Author_API.Dtos;
using Author_API.Entities;
using Author_API.Paging;
using Author_API.Repositories;
using BussinessAccessLayer;
using BussinessAccessLayer.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {

        private readonly AuthorManager _authorManager;


        public AuthorController(AuthorManager authorManager)
        {
            _authorManager = authorManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResource>>> GetAsync([FromQuery] PagingParameters pagingParameters)
        {
            if (pagingParameters.SearchName.Any(char.IsDigit))
            {
                return BadRequest("Name Shouln't Contain Numerics!");
            }

            var result = await _authorManager.GetAsync(pagingParameters);
            return Ok(result);
        }
    
        [HttpGet("{Id}")]
        public async Task<ActionResult<AuthorResource>> GetByIdAsync(int Id)
        {
            var result = await _authorManager.GetByIdAsync(Id);
            if (result is null)
            {
                return NotFound("There is No Authors With Such Id");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task <ActionResult<AuthorResource>> CreateAsync(AuthorModel Model)
        {

            if (Validate(Model))
            {
                var result = await _authorManager.CreateAsync(Model);
                 return Ok(result);
            };
            return BadRequest("Either Email Or Phone Number Must Be Provided");
        }

        [HttpPut("{Id}")]
        public async Task <ActionResult> UpdateAsync(int Id, AuthorModel Model)
        {
            var exsistingAuthor = await _authorManager.GetByIdAsync(Id);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }

            if (Validate(Model))
            {
                await _authorManager.UpdateAsync(Id,Model);
                return Ok();
            }
            return BadRequest("Either Email Or Phone Number Must Be Provided");
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync(int Id)
        {
            var exsistingAuthor = await _authorManager.GetByIdAsync(Id);

            if (exsistingAuthor is null)
            {
                return NotFound();
            }

            await _authorManager.DeleteAsync(Id);
            return Ok();
        }
        private static bool Validate(AuthorModel Model)
        {
            return !(String.IsNullOrEmpty(Model.Email) && String.IsNullOrEmpty(Model.PhoneNumber));
        }
    }
}

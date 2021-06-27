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
            var result = await _authorManager.GetAsync(pagingParameters);
            return Ok(result);
        }
    
        [HttpGet("{Id}")]
        public async Task<ActionResult<AuthorResource>> GetByIdAsync(int Id)
        {
            var result = await _authorManager.GetByIdAsync(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task <ActionResult<AuthorResource>> CreateAsync(AuthorModel Model)
        {
            var result = await _authorManager.CreateAsync(Model);
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task <ActionResult> UpdateAsync(int Id, AuthorModel Model)
        {
            await _authorManager.UpdateAsync(Id,Model);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync(int Id)
        {
            await _authorManager.DeleteAsync(Id);
            return Ok();
        }
    }
}

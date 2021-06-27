using Author_API.Entities;
using Author_API.Models;
using Author_API.Paging;
using Author_API.Repositories;
using Author_API.Resources;
using BussinessAccessLayer.Managers;
using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookManager _bookManager;


        public BookController(BookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResource>>> GetAsync([FromQuery] PagingParameters pagingParameters)
        {
            var result = await _bookManager.GetAsync(pagingParameters);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BookResource>> GetByIdAsync(int Id)
        {
            var result = await _bookManager.GetByIdAsync(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookResource>> CreateAsync(BookModel Model)
        {
            var result = await _bookManager.CreateAsync(Model);
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAsync(int Id, BookModel Model)
        {
            await _bookManager.UpdateAsync(Id, Model);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync(int Id)
        {
            await _bookManager.DeleteAsync(Id);
            return Ok();
        }
    }
}

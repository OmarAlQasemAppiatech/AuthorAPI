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
    public class PublisherController : ControllerBase
    {
        private readonly PublisherManager _publisherManager;
        public PublisherController(PublisherManager publisherManager)
        {
            _publisherManager = publisherManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherResource>>> GetAsync([FromQuery] PagingParameters pagingParameters)
        {
            if (pagingParameters.SearchName.Any(char.IsDigit))
            {
                return BadRequest("Name Shouln't Contain Numerics!");
            }
            var result = await _publisherManager.GetAsync(pagingParameters);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PublisherResource>> GetByIdAsync(int Id)
        {
            var result = await _publisherManager.GetByIdAsync(Id);

            if (result is null)
            {
                return NotFound("There is No Publisher With Such Id");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PublisherResource>> CreateAsync(PublisherModel Model)
        {
            var result = await _publisherManager.CreateAsync(Model);
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAsync(int Id, PublisherModel Model)
        {
            var exsistingPublisher = await _publisherManager.GetByIdAsync(Id);

            if (exsistingPublisher is null)
            {
                return NotFound();
            }
            await _publisherManager.UpdateAsync(Id, Model);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync(int Id)
        {
            var exsistingPublisher = await _publisherManager.GetByIdAsync(Id);

            if (exsistingPublisher is null)
            {
                return NotFound();
            }

            await _publisherManager.DeleteAsync(Id);
            return Ok();
        }
    }
}

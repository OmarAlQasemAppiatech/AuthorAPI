using Author_API.Entities;
using Author_API.Models;
using Author_API.Paging;
using Author_API.Repositories;
using Author_API.Resources;
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
        private readonly IPublishersRepository _publisherRepository;
        private readonly IBooksRepository _bookRepository;

        public PublisherController(IPublishersRepository publisherRepository, IBooksRepository bookRepository)
        {
            _publisherRepository = publisherRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherResource>>> GetAsync([FromQuery] PagingParameters pagingParameters)
        {
            if (pagingParameters.SearchName.Any(char.IsDigit))
            {
                return BadRequest("Name Shouln't Contain Numerics!");
            }
            var Publishers = (await _publisherRepository.GetAsync(pagingParameters)).Select(Publisher => Publisher.PublisherAsResource()).OrderBy(x => x.Id);
            return Ok(Publishers);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PublisherResource>> GetByIdAsync(int Id)
        {
            var Publisher = await _publisherRepository.GetByIdAsync(Id);

            if (Publisher is null)
            {
                return NotFound("There is No Publisher With Such Id");
            }

            return Ok(Publisher.PublisherAsResource());
        }

        [HttpPost]
        public async Task<ActionResult<PublisherResource>> CreateAsync(PublisherModel Model)
        {
            PagingParameters pagingParameters = new PagingParameters();
            var AllBooks = await _bookRepository.GetAsync(pagingParameters);
            Publisher Publisher = new()
            {
                Id = new(),
                Name = Model.Name,
                Email = Model.Email,
                Address = Model.Address,
                PhoneNumber = Model.PhoneNumber,
            };
            Publisher.Books = AllBooks.Where(x => x.Publisher.Id == Publisher.Id).ToList();

                await _publisherRepository.CreateAsync(Publisher);
                return CreatedAtAction(nameof(GetByIdAsync), new { Id = Publisher.Id }, Publisher.PublisherAsResource());
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAsync(int Id, PublisherModel Model)
        {
            var exsistingPublisher = await _publisherRepository.GetByIdAsync(Id);

            if (exsistingPublisher is null)
            {
                return NotFound();
            }



            exsistingPublisher.Name = Model.Name;
            exsistingPublisher.Email = Model.Email;
            exsistingPublisher.Address = Model.Address;
            exsistingPublisher.PhoneNumber = Model.PhoneNumber;

                await _publisherRepository.UpdateAsync(exsistingPublisher);
                return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync(int Id)
        {
            var exsistingPublisher = await _publisherRepository.GetByIdAsync(Id);

            if (exsistingPublisher is null)
            {
                return NotFound();
            }

            await _publisherRepository.DeleteAsync(exsistingPublisher);
            return NoContent();
        }
    }
}

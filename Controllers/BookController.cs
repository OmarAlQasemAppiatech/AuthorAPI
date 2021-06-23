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
    public class BookController : ControllerBase
    {
        private readonly IBooksRepository _bookRepository;
        private readonly IAuthorsRepository _Authorsrepository;
        private readonly IPublishersRepository _publishersRepository;

        public BookController(IBooksRepository bookRepository , IAuthorsRepository authorsRepository, IPublishersRepository publishersRepository)
        {
            _bookRepository = bookRepository;
            _Authorsrepository = authorsRepository;
            _publishersRepository = publishersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResource>>> GetAsync([FromQuery] PagingParameters pagingParameters)
        {
            if (pagingParameters.SearchName.Any(char.IsDigit))
            {
                return BadRequest("Name Shouln't Contain Numerics!");
            }
            var Books = await _bookRepository.GetAsync(pagingParameters);
            return Ok(Books.Select(Book => Book.BookAsResource()).OrderBy(x => x.Id));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BookResource>> GetByIdAsync(int Id)
        {
            var Book = await _bookRepository.GetByIdAsync(Id);

            if (Book is null)
            {
                return NotFound("There is No Books With Such Id");
            }

            return Ok(Book.BookAsResource());
        }

        [HttpPost]
        public async Task<ActionResult<BookResource>> CreateAsync(BookModel Model)
        {
            PagingParameters pagingParameters = new  PagingParameters();
            var AllAuthors = await _Authorsrepository.GetAsync(pagingParameters);
            var Authors = AllAuthors.Where(item => Model.AuthorsIds.Contains(item.Id)).ToList();

            var Publisher = await _publishersRepository.GetByIdAsync(Model.PublisherId);
            Book Book = new()
            {
                Id = new(),
                Title = Model.Title,
                DateOfPublish = Model.DateOfPublish,
                NumberOfPages = Model.NumberOfPages,
                Publisher = Publisher,
                Authors = Authors
            };
            await _bookRepository.CreateAsync(Book);
            return CreatedAtAction(nameof(GetByIdAsync), new { Id = Book.Id }, Book.BookAsResource());
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAsync(int Id, BookModel Model)
        {
            PagingParameters pagingParameters = new PagingParameters();
            var authors = await _Authorsrepository.GetAsync(pagingParameters);
            var Authors = authors.Where(item => Model.AuthorsIds.Contains(item.Id)).ToList();

            var Publisher = await _publishersRepository.GetByIdAsync(Model.PublisherId);

            var exsistingBook = await _bookRepository.GetByIdAsync(Id);

            if (exsistingBook is null)
            {
                return NotFound();
            }

            {

                exsistingBook.Title = Model.Title;
                exsistingBook.DateOfPublish = Model.DateOfPublish;
                exsistingBook.NumberOfPages = Model.NumberOfPages;
                exsistingBook.Publisher = Publisher;
                exsistingBook.Authors = Authors;


                await _bookRepository.UpdateAsync(exsistingBook);
                return NoContent();

            }
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync(int Id)
        {
            var exsistingBook = await _bookRepository.GetByIdAsync(Id);

            if (exsistingBook is null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteAsync(exsistingBook);
            return NoContent();
        }
    }
}

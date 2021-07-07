using Author_API.Dtos;
using Author_API.Entities;
using Author_API.Models;
using Author_API.Paging;
using Author_API.Repositories;
using Author_API.Resources;
using Contract;
using Contract.Models;
using Contract.Resources;
using DataAccessLayer.Repositories;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Author_API.Middlewares.Exceptions;

namespace BussinessAccessLayer.Managers
{
    public class BookManager
    {
        private readonly IAuthorsRepository _authorsrepository;
        private readonly IBooksRepository _bookRepository;
        private readonly IPublishersRepository _publishersRepository;


        public BookManager(IAuthorsRepository repository, IBooksRepository booksRepository, IPublishersRepository publishersRepository)
        {
            _authorsrepository = repository;
            _bookRepository = booksRepository;
            _publishersRepository = publishersRepository;

        }

        //Get All Books
        public async Task<IEnumerable<BookResource>> GetAsync(PagingParameters pagingParameters)
        {
            if (pagingParameters.SearchName.Any(char.IsDigit))
            {
                throw new BadRequestException("Title Shouln't Contain Numerics!");
            }
            var Books = (await _bookRepository.GetAsync(pagingParameters)).Select(Book => Book.BookAsResource()).OrderBy(x => x.Id);
            return Books;
        }

        //Get Book By ID
        public async Task<BookResource> GetByIdAsync(int Id)
        {
            var Book = await _bookRepository.GetByIdAsync(Id);
            if (Book == null)
            {
                throw new NotFoundException($"there is No Book With Id : {Id}");
            }
            return Book.BookAsResource();
        }

        //Create Author
        public async Task<BookResource> CreateAsync(BookModel Model)
        {
            PagingParameters pagingParameters = new();
            var AllAuthors = await _authorsrepository.GetAsync(pagingParameters);
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
            return Book.BookAsResource();
        }

        //Update Author
        public async Task UpdateAsync(int Id, BookModel Model)
        {
            PagingParameters pagingParameters = new();
            var authors = await _authorsrepository.GetAsync(pagingParameters);
            var Authors = authors.Where(item => Model.AuthorsIds.Contains(item.Id)).ToList();

            var Publisher = await _publishersRepository.GetByIdAsync(Model.PublisherId);

            var exsistingBook = await _bookRepository.GetByIdAsync(Id);
            if (exsistingBook == null)
            {
                throw new NotFoundException($"There is No Book With Id : {Id}");
            }

            {

                exsistingBook.Title = Model.Title;
                exsistingBook.DateOfPublish = Model.DateOfPublish;
                exsistingBook.NumberOfPages = Model.NumberOfPages;
                exsistingBook.Publisher = Publisher;
                exsistingBook.Authors = Authors;


                await _bookRepository.UpdateAsync(exsistingBook);
            }
        }

        //Delete Author
        public async Task DeleteAsync(int Id)
        {
            {
                var exsistingBook = await _bookRepository.GetByIdAsync(Id);
                if (exsistingBook == null)
                {
                    throw new NotFoundException($"There is No Book With Id : {Id}");
                }
                await _bookRepository.DeleteAsync(exsistingBook);
            }
        }
    }
}
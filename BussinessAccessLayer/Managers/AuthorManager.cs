using Author_API.Dtos;
using Author_API.Entities;
using Author_API.Paging;
using Author_API.Repositories;
using Contract;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Author_API.Middlewares.Exceptions;

namespace BussinessAccessLayer.Managers
{
    public class AuthorManager
    {
        private readonly IAuthorsRepository _repository;
        private readonly IBooksRepository _bookRepository;



        public AuthorManager(IAuthorsRepository repository , IBooksRepository booksRepository)
        {
            _repository = repository;
            _bookRepository = booksRepository;

        }

        //Get All Authors
        public async Task<IEnumerable<AuthorResource>> GetAsync(PagingParameters pagingParameters)
        {
            if (pagingParameters.SearchName.Any(char.IsDigit))
            {
                throw new BadRequestException("Name Shouln't Contain Numerics!");
            }
            var Authors = (await _repository.GetAsync(pagingParameters)).Select(Author => Author.AsResource()).OrderBy(x => x.Id);
            return Authors;
        }

        //Get Author By ID
        public async Task<AuthorResource> GetByIdAsync(int Id)
        {
            var Author = await _repository.GetByIdAsync(Id);
            if ( Author == null)
            {
                throw new NotFoundException($"there is No Author With Id : {Id}");
            }
            return Author.AsResource();
        }

        //Create Author
        public async Task<AuthorResource> CreateAsync(AuthorModel Model)
        {
            PagingParameters pagingParameters = new PagingParameters();
            var AllBooks = await _bookRepository.GetAsync(pagingParameters);
            if (Validate(Model))
            {
                Author Author = new()
                {
                    Id = new(),
                    Name = Model.Name,
                    Email = Model.Email,
                    DateOfBirth = Model.DateOfBirth,
                    PhoneNumber = Model.PhoneNumber
                };
                Author.Books = AllBooks.Where(x => x.Authors.Contains(Author)).ToList();
                await _repository.CreateAsync(Author);
                return Author.AsResource();
            }
            throw new BadRequestException("Either Email Or Phone Number Must Be Provided");
        }

        //Update Author
        public async Task UpdateAsync(int Id, AuthorModel Model)
        {
            var exsistingAuthor = await _repository.GetByIdAsync(Id);
            if (exsistingAuthor == null)
            {
                throw new NotFoundException($"there is No Author With Id : {Id}");
            }
            if (Validate(Model))
            {
                exsistingAuthor.Name = Model.Name;
                exsistingAuthor.Email = Model.Email;
                exsistingAuthor.DateOfBirth = Model.DateOfBirth;
                exsistingAuthor.PhoneNumber = Model.PhoneNumber;

                await _repository.UpdateAsync(exsistingAuthor);
            }
            else
                throw new BadRequestException("Either Email Or Phone Number Must Be Provided");
        }

        //Delete Author
        public async Task DeleteAsync(int Id)
        {
            var exsistingAuthor = await _repository.GetByIdAsync(Id);
            if (exsistingAuthor == null)
            {
                throw new NotFoundException($"there is No Author With Id : {Id}");
            }
            await _repository.DeleteAsync(exsistingAuthor);
        }

        //Validation for At Least Email Or Phone Number 
        private static bool Validate(AuthorModel Model)
        {
            return !(String.IsNullOrEmpty(Model.Email) && String.IsNullOrEmpty(Model.PhoneNumber));
        }
    }
}

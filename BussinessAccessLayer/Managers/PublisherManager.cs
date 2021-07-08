using Author_API.Entities;
using Author_API.Models;
using Author_API.Paging;
using Author_API.Repositories;
using Author_API.Resources;
using Contract;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Author_API.Middlewares.Exceptions;

namespace BussinessAccessLayer.Managers
{
    public class PublisherManager
    {
        private readonly IPublishersRepository _publisherRepository;
        private readonly IBooksRepository _bookRepository;

        public PublisherManager(IPublishersRepository publisherRepository, IBooksRepository bookRepository)
        {
            _publisherRepository = publisherRepository;
            _bookRepository = bookRepository;
        }

        //Get All Publishers
        public async Task<IEnumerable<PublisherResource>> GetAsync(PagingParameters pagingParameters)
        {
            if (pagingParameters.SearchName.Any(char.IsDigit))
            {
                throw new BadRequestException("Name Shouln't Contain Numerics!");
            }
            var Publishers = (await _publisherRepository.GetAsync(pagingParameters)).Select(Publisher => Publisher.PublisherAsResource()).OrderBy(x => x.Id);
            return Publishers;
        }

        //Get Publisher By ID
        public async Task<PublisherResource> GetByIdAsync(int Id)
        {
            var Publisher = await _publisherRepository.GetByIdAsync(Id);
            if (Publisher == null)
            {
                throw new NotFoundException($"There is No Publisher With Id : {Id}");
            }
            return Publisher.PublisherAsResource();
        }

        //Create Publisher
        public async Task<PublisherResource> CreateAsync(PublisherModel Model)
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
            return Publisher.PublisherAsResource();
        }

        //Update Publisher
        public async Task UpdateAsync(int Id, PublisherModel Model)
        {
            var exsistingPublisher = await _publisherRepository.GetByIdAsync(Id);
            if (exsistingPublisher == null)
            {
                throw new NotFoundException($"There is No Publisher With Id : {Id}");
            }
            exsistingPublisher.Name = Model.Name;
            exsistingPublisher.Email = Model.Email;
            exsistingPublisher.Address = Model.Address;
            exsistingPublisher.PhoneNumber = Model.PhoneNumber;

            await _publisherRepository.UpdateAsync(exsistingPublisher);
        }

        //Delete Publisher
        public async Task DeleteAsync(int Id)
        {
            var exsistingPublisher = await _publisherRepository.GetByIdAsync(Id);
            if (exsistingPublisher == null)
            {
                throw new NotFoundException($"There is No Publisher With Id : {Id}");
            }
            await _publisherRepository.DeleteAsync(exsistingPublisher);
        }
    }
}

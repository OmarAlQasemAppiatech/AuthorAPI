using Author_API.Entities;
using Author_API.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Repositories
{
        public interface IPublishersRepository
        {
        Task<PagedList<Publisher>> GetAsync(PagingParameters pagingParameters);

        Task<Publisher> GetByIdAsync(int PublisherId);

            Task CreateAsync(Publisher Publisher);

            Task UpdateAsync(Publisher Publisher);

            Task DeleteAsync(Publisher Publisher);

        }
        public class PublishersRepository : IPublishersRepository
        {
            private readonly AuthorsDbContext _context;

            public PublishersRepository(AuthorsDbContext context)
            {
                _context = context;
            }

        public async Task<PagedList<Publisher>> GetAsync(PagingParameters pagingParameters)
        {
            var result = await _context.Publishers.Include(x => x.Books).ToListAsync();

            var searchResult = result.Where(x => x.Name.StartsWith(pagingParameters.SearchName)).OrderBy(x => x.Name);

            return await Task.FromResult(PagedList<Publisher>.GetPagedList(searchResult, pagingParameters.PageNumber, pagingParameters.PageSize));
        }

        public async Task<Publisher> GetByIdAsync(int PublisherId)
            {
                var Publishers = await _context.Publishers.ToListAsync();
                return Publishers.FirstOrDefault(Publisher => Publisher.Id == PublisherId);
            }

            public async Task CreateAsync(Publisher Publisher)
            {
                await _context.Publishers.AddAsync(Publisher);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Publisher Publisher)
            {
                _context.Publishers.Update(Publisher);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Publisher Publisher)
            {
                _context.Remove(Publisher);
                await _context.SaveChangesAsync();
            }
        }
   
}

using Author_API.Entities;
using Author_API.Paging;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IBooksRepository
    {
        Task<Book> GetByIdAsync(int BookId);

        Task<PagedList<Book>> GetAsync(PagingParameters pagingParameters);

        Task CreateAsync(Book Book);

        Task UpdateAsync(Book Book);

        Task DeleteAsync(Book Book);

    }
    public class BooksRepository : IBooksRepository
    {
        private readonly AuthorsDbContext _context;
        public BooksRepository(AuthorsDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Book Book)
        {
            await _context.Books.AddAsync(Book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book Book)
        {
            _context.Remove(Book);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedList<Book>> GetAsync(PagingParameters pagingParameters)
        {
            var result = _context.Books.Include(x => x.Publisher).Include(x => x.Authors).Where(x => x.Title.StartsWith(pagingParameters.SearchName));

            var searchResult = await result.OrderBy(x => x.Title).ToListAsync();

            return await Task.FromResult(PagedList<Book>.GetPagedList(searchResult, pagingParameters.PageNumber, pagingParameters.PageSize));
        }

        public async Task<Book> GetByIdAsync(int BookId)
        {
            var Books = await _context.Books.Include(x => x.Publisher).Include(x => x.Authors).ToListAsync();
            return Books.FirstOrDefault(Book => Book.Id == BookId);
        }

        public async Task UpdateAsync(Book Book)
        {
            _context.Books.Update(Book);
            await _context.SaveChangesAsync();
        }
    }
}

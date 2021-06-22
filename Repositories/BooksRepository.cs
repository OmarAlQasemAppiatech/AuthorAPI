using Author_API.Entities;
using Author_API.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Repositories
{
    public interface IBooksRepository
    {
        Task<Book> GetByIdAsync(int BookId);

        Task<IEnumerable<Book>> GetAsync();

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

        public async Task<IEnumerable<Book>> GetAsync()
        {
            return await _context.Books.Include(x => x.Publisher).Include(x=>x.Authors).ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int BookId)
        {
            var Books = await _context.Books.ToListAsync();
            return Books.FirstOrDefault(Book => Book.Id == BookId);
        }

        public async Task UpdateAsync(Book Book)
        {
            _context.Books.Update(Book);
            await _context.SaveChangesAsync();
        }
    }
}

using Author_API.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Repositories
{
        public interface IAuthorsRepository
    {
        Task <Author> GetAuthorAsync(int AuthorId);

        Task<IEnumerable<Author>> GetAuthorsAsync();

        Task CreateAuthorAsync(Author Author);

        Task UpdateAuthorAsync(Author Author);

        Task DeleteAuthorAsync(Author author);

    }
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly AuthorsDbContext _context;

        public AuthorsRepository(AuthorsDbContext context)
        {
            _context = context;
        }

        public async Task <IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task <Author> GetAuthorAsync(int AuthorId)
        {
             var Authors= await _context.Authors.ToListAsync();
            return Authors.FirstOrDefault(Author => Author.Id == AuthorId);
        }

        public async Task CreateAuthorAsync(Author Author)
        {
            await _context.Authors.AddAsync(Author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(Author author)
        {
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}

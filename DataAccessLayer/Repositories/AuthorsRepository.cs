using Author_API.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Author_API.Paging;

namespace Author_API.Repositories
{
        public interface IAuthorsRepository
    {
        Task<PagedList<Author>> GetAsync(PagingParameters pagingParameters);

        Task<Author> GetByIdAsync(int AuthorId);

        Task CreateAsync(Author Author);

        Task UpdateAsync(Author Author);

        Task DeleteAsync(Author author);

    }
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly AuthorsDbContext _context;

        public AuthorsRepository(AuthorsDbContext context)
        {
            _context = context;
        }

        public async Task <PagedList<Author>> GetAsync(PagingParameters pagingParameters)
        {
            var result = _context.Authors.Where(x => x.Name.StartsWith(pagingParameters.SearchName));

            var searchResult = await result.OrderBy(x => x.Name).ToListAsync();

            return await Task.FromResult(PagedList<Author>.GetPagedList(searchResult, pagingParameters.PageNumber, pagingParameters.PageSize));
        }

        public async Task <Author> GetByIdAsync(int AuthorId)
        {
             var Authors= await _context.Authors.ToListAsync();
            return Authors.FirstOrDefault(Author => Author.Id == AuthorId);
        }

        public async Task CreateAsync(Author Author)
        {
            await _context.Authors.AddAsync(Author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Author author)
        {
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}

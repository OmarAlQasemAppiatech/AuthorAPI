using Author_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly AuthorsDbContext _context;

        public AuthorsRepository(AuthorsDbContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<Author> GetAuthors()
        {
        
            return _context.Authors.ToList();
        }
        public Author GetAuthor(int AuthorId)
        {
            return _context.Authors.ToList().SingleOrDefault(Author => Author.AuthorId == AuthorId);
        }

        public void CreateAuthor(Author Author)
        {
            _context.Authors.Add(Author);
            _context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
        }

        public void DeleteAuthor(Author author)
        {
            _context.Remove(author);
            _context.SaveChanges();
        }
    }
}

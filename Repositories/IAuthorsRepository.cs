using Author_API.Entities;
using System;
using System.Collections.Generic;

namespace Author_API.Repositories
{
    public interface IAuthorsRepository
    {
        Author GetAuthor(int AuthorId);
        IEnumerable<Author> GetAuthors();

        void CreateAuthor(Author Author);
        void UpdateAuthor(Author Author);
        void DeleteAuthor(Author author);

    }
}
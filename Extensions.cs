using Author_API.Dtos;
using Author_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API
{
    public static class Extensions
    {
        public static AuthorDto AsDto(this Author Author)
        {
            return new AuthorDto
            {
                AuthorId = Author.AuthorId,
                Name = Author.Name,
                Email = Author.Email,
                DateOfBirth = Author.DateOfBirth
            };
        }
    }
}

﻿using Author_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Author_API.Dtos;

namespace Author_API
{
    public static class Extensions
    {
        public static AuthorResource AsResource(this Author Author)
        {
            return new AuthorResource
            {
                AuthorId = Author.AuthorId,
                Name = Author.Name,
                Email = Author.Email,
                DateOfBirth = Author.DateOfBirth
            };
        }
    }
}

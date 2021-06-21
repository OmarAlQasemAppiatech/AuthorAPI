using Author_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Author_API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Author_API.Resources;

namespace Author_API
{
    public static class Extensions
    {
        public static AuthorResource AsResource(this Author Author)
        {
            return new AuthorResource
            {
                Id = Author.Id,
                Name = Author.Name,
                Email = Author.Email,
                DateOfBirth = Author.DateOfBirth,
                PhoneNumber = Author.PhoneNumber,
                Age= DateTime.Now.Year - Author.DateOfBirth.Year
            };
        }
        public static PublisherResource PublisherAsResource(this Publisher Publisher)
        {
            return new PublisherResource
            {
                Id = Publisher.Id,
                Name = Publisher.Name,
                Address = Publisher.Address,
                Email =Publisher.Email,
                PhoneNumber=Publisher.PhoneNumber,
                Books=Publisher.Books?.Select(x=>x.PublisherBookAsResource()).ToList()
            };
        }
        public static BookResource BookAsResource(this Book Book)
        {
            return new BookResource
            {
                Id = Book.Id,
                Title = Book.Title,
                DateOfPublish = Book.DateOfPublish,
                NumberOfPages = Book.NumberOfPages,
                Publisher = Book.Publisher?.PublisherAsResource(),
                Authors=Book.Authors?.Select(x => x.AsResource()).ToList(),
            };
        }

        public static AuthorBookResource AuthorBookAsResource(this Book Book)
        {
            return new AuthorBookResource
            {
                Id = Book.Id,
                Title = Book.Title,
            };
        }
        public static PublisherBookResource PublisherBookAsResource(this Book Book)
        {
            return new PublisherBookResource
            {
                Id = Book.Id,
                Title = Book.Title,
            };
        }
    }
}

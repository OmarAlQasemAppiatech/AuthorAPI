using Author_API.Dtos;
using Author_API.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Resources
{
    public class BookResource
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateOfPublish { get; set; }

        public int NumberOfPages { get; set; }

        public BookPublisherResource Publisher { get; set; }

        public ICollection<BookAuthorResource> Authors { get; set; }

    }
}

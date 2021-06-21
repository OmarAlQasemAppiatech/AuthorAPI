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
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfPublish { get; set; }

        public int NumberOfPages { get; set; }

        public PublisherResource Publisher { get; set; }

        public ICollection<AuthorResource> Authors { get; set; }

    }
}

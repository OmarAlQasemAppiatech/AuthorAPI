using Author_API.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Contract.Resources
{
    public class BookResource
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime DateOfPublish { get; set; }

        public int NumberOfPages { get; set; }

        public BookPublisherResource Publisher { get; set; }

        public ICollection<BookAuthorResource> Authors { get; set; }
    }
}

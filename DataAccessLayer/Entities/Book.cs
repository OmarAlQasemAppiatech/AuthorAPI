using Author_API.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfPublish { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [Required]
        public ICollection<Author> Authors { get; set; }

        [Required]
        public Publisher Publisher { get; set; }
    }
}

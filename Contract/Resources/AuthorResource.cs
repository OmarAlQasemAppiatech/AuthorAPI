using Author_API.Entities;
using Author_API.Resources;
using Contract.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Dtos
{
    public class AuthorResource
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public int Age { get; set; }
        public ICollection<AuthorBookResource> Books { get; set; }

    }
}

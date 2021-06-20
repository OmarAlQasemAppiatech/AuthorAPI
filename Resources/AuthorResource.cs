using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Dtos
{
    public class AuthorResource
    {
        [Key]
        public int Id { get; init; }

        public string Name { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email Should be in a valid format, e.g. Sample@mail.com")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "Phone Number Must Only Include Numbers and Be 10 Digits At Most!")]
        public string PhoneNumber { get; set; }

        public int Age { get; set; }
    }
}

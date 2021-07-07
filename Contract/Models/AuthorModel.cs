using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Author_API.Entities
{
    public class AuthorModel
    {
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email Should be in a valid format, e.g. Sample@mail.com")]
        public string Email { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "Phone Number Must Only Include Numbers and Be 10 Digits At Most!")]
        public string PhoneNumber { get; set; }
    }
}

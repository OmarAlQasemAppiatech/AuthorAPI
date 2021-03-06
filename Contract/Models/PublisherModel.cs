using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Models
{
    public class PublisherModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email Should Be In A Valid Format, e.g. Sample@mail.com")]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "Phone Number Must Only Include Numbers and Be 10 Digits At Most!")]
        public string PhoneNumber { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Author_API.Entities
{
    public class Publisher
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email Should be in a valid format, e.g. Sample@mail.com")]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "Phone Number Must Only Include Numbers and Be 10 Digits At Most!")]
        public string PhoneNumber { get; set; }

        public ICollection<Book> Books { get; set; }

    }
}
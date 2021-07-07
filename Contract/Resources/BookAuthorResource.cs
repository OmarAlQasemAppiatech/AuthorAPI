using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Resources
{
    public class BookAuthorResource
    {
        public int Id { get; init; }

        public string Name { get; set; }
    }
}
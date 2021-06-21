using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Resources
{
    public class PublisherBookResource
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}

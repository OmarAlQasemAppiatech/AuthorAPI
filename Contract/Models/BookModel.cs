using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Models
{
    public class BookModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfPublish { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [Required(ErrorMessage = "Publisher Id Can't Be Null!")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Authors Ids Can't Be Null!")]
        public List<int> AuthorsIds { get; set; }
    }
}

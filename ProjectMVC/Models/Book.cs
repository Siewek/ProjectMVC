using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; } // isbn number

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Description { get; set; }

        public int Ammount { get; set; }

        //foreign key for author
        public virtual IList<AuthorBook> BooksByAuthor { get; set; }

        public Category Category { get; set; }

        public int CategoryFK { get; set; }

        public virtual IList<BookTag> BooksByTags { get; set; }


        public string TableOfContents { get; set; }
    }
}


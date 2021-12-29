using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        public string AuthorName { get; set; }

        public string AuthorLastName { get; set; }

        public virtual IList<AuthorBook> BooksByAuthor { get; set; }

    }
}

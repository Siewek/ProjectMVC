using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class AuthorBook
    {
        public int AuthorID { get; set; }
        public Author Author { get; set; }
        public int BookID { get; set; }

        public Book Book { get; set; }
    }
}

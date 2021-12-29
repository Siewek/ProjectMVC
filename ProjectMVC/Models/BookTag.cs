using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class BookTag
    {
        public int BookID { get; set; }

        public Book Book { get; set; }

        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }
}

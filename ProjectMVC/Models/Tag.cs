using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class Tag
    {
        [Key]
        public int TagID { get; set; }

        public string TagName { get; set; }

        public virtual IList<BookTag> BooksByTags { get; set; }

    }
}

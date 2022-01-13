using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string BookID { get; set; }
        public string Title { get; set; }
        public string UserID { get; set; }

        public DateTime DateOfOrder { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateOfReturn { get; set; }
        public bool fulfilled { get; set; }

        public bool returned { get; set; }

        public bool returnedInTime { get; set; }

        public int penalty { get; set; }

    }
}

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
        public int BookID { get; set; }
        public int UserID { get; set; }


    }
}

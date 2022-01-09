using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class AdminMessage
    {
        [Key]
        public int AdMesID { get; set; }

        [MaxLength(1000)]
        [Required]
        public string mesage { get; set; }

        public DateTime timestamp { get; set; }
    }
}

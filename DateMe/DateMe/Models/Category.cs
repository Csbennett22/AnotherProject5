using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DateMe.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }
        // public string MajorCode { get; set; }
        public string CategoryName { get; set; }
    }
}
}

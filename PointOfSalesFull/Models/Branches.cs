using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Branches
    {
        [Key]
        [Required]
        public int Branch_ID { get; set; }
        [Required]
        public string Branch_Name { get; set; }
    
        public string Branch_EName { get; set; }
    }
}
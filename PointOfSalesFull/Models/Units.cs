using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Units
    {
        [Key]
        [Required]
        public int Unit_ID { get; set; }
        [Required]
        public string Unit_Name { get; set; }
        [Required]
        public string Unit_EName { get; set; }
    }
}
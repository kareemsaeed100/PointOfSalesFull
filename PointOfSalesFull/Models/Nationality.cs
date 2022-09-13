using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Nationality
    {
        [Key]
        [Required]
        public int Nat_ID { get; set; }
        [Required]
        public string Nat_Name { get; set; }
        public string Nat_EName { get; set; }
    }
}
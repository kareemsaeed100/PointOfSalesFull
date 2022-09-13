using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Brand
    {
        [Key]
        [Required]
        public int Brand_ID { get; set; }
        [Required]
        public string Brand_Name { get; set; }

        public string Brand_EName { get; set; }
    }
}
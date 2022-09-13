using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Stock
    {
        [Key]
        [Required]
        public int Stock_ID { get; set; }
        [Required]
        public string Stock_Name { get; set; }

        public int Prod_Id { get; set; }
        public int Prod_Quintity{ get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class AccStock
    {
        [Key]
        [Required]
        public int Stock_ID { get; set; }
        [Required]
        public DateTime MovDate { get; set; }
        [Required]
        public int ProductId{ get; set; }
        [Required]
        public int Quintity { get; set; }
        [Required]
        public string MoveType { get; set; }

    }
}
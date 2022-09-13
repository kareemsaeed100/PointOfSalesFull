using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Sales_Detailes
    {
        [Key]
        public int Sales_Det_Key { get; set; }
        public int Sales_ID { get; set; }
        public string Product_ID { get; set; }
        public int Quintity { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public float discount { get; set; }
        public float Tax { get; set; }
        public float Total { get; set; }
        [ForeignKey(nameof(Product_ID))]
        public virtual Product product { get; set; }
        [ForeignKey(nameof(Sales_ID))]
        public virtual SalesInvoice Sales { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class ProductViewModel
    {
        public int Order_D_key { get; set; }
        public string Product_Id{ get; set; }
        public string Product_Name { get; set; }
        public int Quintity { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public float discount { get; set; }
        public float Tax { get; set; }
        public float Total { get; set; }
        public float dr { get; set; }
        public float cr { get; set; }
        public int Order_Id { get; set; }
    }
}
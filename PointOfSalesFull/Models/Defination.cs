using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Defination
    {
        [Key]
        public int Key { get; set; }
        public string AccCustomer { get; set; }
        public string AccCash{ get; set; }
        public string AccSalesRevenues { get; set; }
        public string AccTax { get; set; }
        public string AccSalesCost { get; set; }
        public string AccInv { get; set; }
        public string AccBank { get; set; }
        public string AccSublier{ get; set; }
        public string PureAcc { get; set; }
        public string PureRAcc { get; set; }
        public string SalesAcc { get; set; }
        public string SalesRAcc { get; set; }
        public string ReciptAcc { get; set; }
        public string PayMentAcc { get; set; }
    }
}
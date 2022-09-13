using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Saller
    {
        [Key]
        [Required]
        public int Saller_ID { get; set; }
        [Required]
        public string Saler_Name { get; set; }
        public string Saler_EName { get; set; }

        public string Saler_Email{ get; set; }

        public string Saler_Phon { get; set; }

        public string Saler_Adress { get; set; }
        public int Nat_ID { get; set; }
        public ICollection<SalesInvoice> Sales_Invoice { get; set; }
        public ICollection<PurchasesInvoice> Purchases_Invoice { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class SalesInvoice
    {
        [Key]
        public int Sales_ID { get; set; }
        public Nullable<System.DateTime> Sales_Date { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<int> Saller_ID { get; set; }
        public int Pay_ID1 { get; set; }
        public int Pay_ID2 { get; set; }
        public float Pay1Val { get; set; }
        public float Pay2Val { get; set; }
        public int Brn_Num { get; set; }
        public ICollection<Sales_Detailes> Sales_Detailes { get; set; }

        [ForeignKey(nameof(Customer_ID))]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(Saller_ID))]
        public virtual Saller Saller { get; set; }
        


    }
}
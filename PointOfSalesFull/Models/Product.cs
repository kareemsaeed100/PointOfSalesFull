using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public partial class Product
    {
        [Key]
        [Required]
        public string Product_ID { get; set; }
        [Required]
        public string Product_Name { get; set; }
        public Nullable<int> Quintity { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public int Cat_ID { get; set; }
        public int Brand_Id { get; set; }
        public int Uint_Id { get; set; }
        public int For_Sale { get; set; }
        public int Tax_State { get; set; }
        public int Return{ get; set; }
        public int Mov_State { get; set; }
    
        public ICollection<Sales_Detailes> Sales_Detailes { get; set; }
        public ICollection<PurchasesDetailes> Purchases_Detailes { get; set; }
        public ICollection<SalesReturnDetailes> SalesReturnDetailes { get; set; }
        public ICollection<PurchasesReturnDetailes> PurchasesReturnDetailes { get; set; }
        [ForeignKey(nameof(Cat_ID))]
        public virtual Category category { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Customes_ID { get; set; }
        [Required]
        public string First_Name { get; set; }
        public int Nat_ID { get; set; }
        public string Last_Name { get; set; }
        public string First_EName { get; set; }
        public string Last_EName { get; set; }
        public string Phon { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public ICollection<SalesInvoice> orders { get; set; }
    }
}
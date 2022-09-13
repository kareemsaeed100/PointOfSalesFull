using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Supplier
    {
        [Key]
        [Required]
        public int Sub_ID { get; set; }
        [Required]
        public string Sub_Name { get; set; }
        public string Sub_EName { get; set; }
   
        public string Sub_Email { get; set; }

        public string Sub_Phon { get; set; }

        public string Sub_Adress { get; set; }
        public int Nat_ID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class PayMode
    {
        [Key]
        [Required]
        public int Pay_ID { get; set; }
        [Required]
        public string Pay_Name { get; set; }
        [Required]
        public string Pay_EName { get; set; }
        [Required]
        public string Pay_Acc { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int Cat_ID { get; set; }
        [Required]
        public string Cat_Descrption { get; set; }
        public string Cat_EDescrption { get; set; }
        public ICollection<Product> Products { get; set; }
        [Required]
        public int Dep_ID { get; set; }
        [ForeignKey(nameof(Dep_ID))]
        public virtual Department department { get; set; }
    }
}
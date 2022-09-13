using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class Department
    {
        [Key]
        [Required]
        public int Deb_ID { get; set; }
        [Required]
        public string Deb_Name { get; set; }
        public string Deb_EName { get; set; }
        public ICollection<Category> Categories { get; set; }
    
}
}
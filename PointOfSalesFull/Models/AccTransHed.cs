using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class AccTransHed
    {
             


        [Key]
        [Required]
        public int thd_Key { get; set; }

        [Required]
        public int thd_num { get; set; }
        [Required]
        public int thd_MovNum { get; set; }
        [Required]
        public string thd_typ { get; set; }
        [Required]
        public int thd_brn { get; set; }

        [Required]
        public DateTime thd_dat { get; set; }
        [Required]
        public string thd_des { get; set; }
        public ICollection<AccTransDet> AccTransDet { get; set; }
    }
}
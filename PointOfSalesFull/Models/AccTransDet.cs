
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class AccTransDet
    {
                       
        [Key]
        [Required]
        public int tdt_Key { get; set; }
        [Required]
        public int thd_num { get; set; }

        [Required]
        public int tdt_num { get; set; }

        [Required]
        public string tdt_lne { get; set; }
        [Required]
        public string tdt_typ { get; set; }
        [Required]
        public int tdt_brn { get; set; }
        [Required]
        public string tdt_L1 { get; set; }
        [Required]
        public string tdt_L2 { get; set; }
        [Required]
        public string tdt_C1 { get; set; }
        [Required]
        public float tdt_dr { get; set; }
        [Required]
        public float tdt_cr { get; set; }
        [Required]
        public DateTime tdt_dat { get; set; }
        [Required]
        public string tdt_des { get; set; }
        [ForeignKey(nameof(thd_num))]
        public virtual AccTransHed AccTransHed { get; set; }
    }
}
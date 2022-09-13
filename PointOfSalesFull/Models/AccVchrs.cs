using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class AccVchrs
    {

        [Key]
        [Required]
        public int vch_num { get; set; }
        [Required]
        public int vch_Brn { get; set; }
        [Required]
        public DateTime vch_DatG { get; set; }

        public int vch_paymod1 { get; set; }
        public int vch_paymod2 { get; set; }
        [Required]
        public int Vch_PurNum { get; set; }
        [Required]
        public int Vch_SubNum { get; set; }
        public string vch_cardnum { get; set; }

        public float vch_Paid1 { get; set; }

        public float vch_Paid2 { get; set; }
        [Required]
        public float vch_Wanted { get; set; }

        public float vch_Remind { get; set; }


    }
}
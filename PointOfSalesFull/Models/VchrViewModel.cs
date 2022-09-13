using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Controllers
{
    public class VchrViewModel
    {
   
        public int vch_num { get; set; }

        public int vch_Brn { get; set; }
        public string vch_BrnName { get; set; }
        public string vch_DatG { get; set; }

        public int vch_paymod1 { get; set; }
        public int vch_paymod2 { get; set; }
        public string vch_paymod1Name { get; set; }
        public string vch_paymod2Name { get; set; }

        public int Vch_PurNum { get; set; }
 
        public int Vch_SubNum { get; set; }
        public string vch_subName { get; set; }
        public string vch_cardnum { get; set; }

        public float vch_Paid1 { get; set; }

        public float vch_Paid2 { get; set; }

        public float vch_Wanted { get; set; }

        public float vch_Remind { get; set; }
    }
}
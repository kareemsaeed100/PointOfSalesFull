using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSalesFull.Models
{
    public class OrderD_viewModel
    {
        public string Email { get; set; }
        public string Order_Date { get; set; }
        public string First_Name { get; set; }
        public string BranchName { get; set; }
        public string PayName { get; set; }

        public string Saler_Name { get; set; }
        public int Order_Id { get; set; }
        //public List<Order_Detailes>O_detailes { get; set; }
    }
}
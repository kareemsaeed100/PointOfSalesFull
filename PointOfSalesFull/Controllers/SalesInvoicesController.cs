using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointOfSalesFull.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using PointOfSalesFull.Report;

namespace PointOfSalesFull.Controllers
{
    public class SalesInvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ExportReportMax()
        {
            var x = db.SalesInvoicies.Max(o => o.Sales_ID);
            List<OrderD_viewModel> ODM = db.SalesInvoicies.Where(o => o.Sales_ID == x).Include(o => o.Sales_Detailes).Include(o => o.Customer).Select(o => new OrderD_viewModel
            {
                Email = o.Customer.Email,
                Order_Date = o.Sales_Date.ToString(),
                First_Name = o.Customer.First_Name,
                Order_Id = o.Sales_ID,
                Saler_Name = o.Saller.Saler_Name,
                BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
            }).ToList();
            List<ProductViewModel> detaileslist = db.SalesDetailes.Where(o => o.Sales_ID == x).Select(o => new ProductViewModel
            {
                Order_D_key = o.Sales_Det_Key,
                Product_Name = o.product.Product_Name,
                Quintity = o.Quintity,
                Price = o.Price,
                Amount = o.Amount,
                discount = o.discount,
                Tax = o.Tax,
                Total = o.Total,
                Order_Id = o.Sales_ID
            }).ToList();
            ReportDocument rd = new salesInvoice();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "SalesInvoice.rpt"));
            rd.Database.Tables[0].SetDataSource(ODM);
            rd.Database.Tables[1].SetDataSource(detaileslist);
           
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "SalesInvoice.pdf");
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult ExportReportSearch(int Sales_num)
        {

            List<OrderD_viewModel> ODM = db.SalesInvoicies.Where(o => o.Sales_ID == Sales_num).Include(o => o.Sales_Detailes).Include(o => o.Customer).Select(o => new OrderD_viewModel
            {
                Email = o.Customer.Email,
                Order_Date = o.Sales_Date.ToString(),
                First_Name = o.Customer.First_Name,
                Order_Id = o.Sales_ID,
                Saler_Name = o.Saller.Saler_Name,
                BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
            }).ToList();
            List<ProductViewModel> detaileslist = db.SalesDetailes.Where(o => o.Sales_ID == Sales_num).Select(o => new ProductViewModel
            {
                Order_D_key = o.Sales_Det_Key,
                Product_Name = o.product.Product_Name,
                Quintity = o.Quintity,
                Price = o.Price,
                Amount = o.Amount,
                discount = o.discount,
                Tax = o.Tax,
                Total = o.Total,
                Order_Id = o.Sales_ID
            }).ToList();
            ReportDocument rd = new salesInvoice();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "SalesInvoice.rpt"));
            rd.Database.Tables[0].SetDataSource(ODM);
            rd.Database.Tables[1].SetDataSource(detaileslist);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "SalesInvoice.pdf");
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult Getorders()
        {
            if (db.SalesInvoicies.Count() > 0)
            {
                var x = db.SalesInvoicies.Max(o => o.Sales_ID);
                List<OrderD_viewModel> ODM = db.SalesInvoicies.Where(o => o.Sales_ID == x).Include(o => o.Sales_Detailes).Include(o => o.Customer).Select(o => new OrderD_viewModel
                {
                    Email = o.Customer.Email,
                    Order_Date = o.Sales_Date.ToString(),
                    First_Name = o.Customer.First_Name,
                    Order_Id = o.Sales_ID,
                    Saler_Name = o.Saller.Saler_Name,
                    BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                    PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
                }).ToList();

                ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customes_ID", "First_Name");
                ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                return Json(ODM, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetorderDetailess()
        {
            if (db.SalesInvoicies.Count() > 0)
            {
                var x = db.SalesInvoicies.Max(o => o.Sales_ID);

                List<ProductViewModel> detaileslist = db.SalesDetailes.Where(o => o.Sales_ID == x).Select(o => new ProductViewModel
                {
                    Order_D_key = o.Sales_Det_Key,
                    Product_Name = o.product.Product_Name,
                    Quintity = o.Quintity,
                    Price = o.Price,
                    Amount = o.Amount,
                    discount = o.discount,
                    Tax = o.Tax,
                    Total = o.Total,
                    Order_Id = o.Sales_ID
                }).ToList();

                ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customes_ID", "First_Name");
                ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                return Json(detaileslist, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // GET: Order_Detailes search
        public JsonResult GetordersHeadSearch(int order_num)
        {
            if (db.SalesInvoicies.Count() > 0)
            {


                List<OrderD_viewModel> ODM = db.SalesInvoicies.Where(o => o.Sales_ID == order_num).Include(o => o.Sales_Detailes).Include(o => o.Customer).Select(o => new OrderD_viewModel
                {
                    Email = o.Customer.Email,
                    Order_Date = o.Sales_Date.ToString(),
                    First_Name = o.Customer.First_Name,
                    Order_Id = o.Sales_ID,
                    Saler_Name = o.Saller.Saler_Name,
                    BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                    PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
                }).ToList();

                ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customes_ID", "First_Name");
                ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                return Json(ODM, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetorderDetailesSearch(int order_num)
        {
            if (db.SalesInvoicies.Count() > 0)
            {


                List<ProductViewModel> detaileslist = db.SalesDetailes.Where(o => o.Sales_ID == order_num).Select(o => new ProductViewModel
                {
                    Order_D_key = o.Sales_Det_Key,
                    Product_Name = o.product.Product_Name,
                    Quintity = o.Quintity,
                    Price = o.Price,
                    Amount = o.Amount,
                    discount = o.discount,
                    Tax = o.Tax,
                    Total = o.Total,
                    Order_Id = o.Sales_ID,
                    Product_Id=o.Product_ID
                }).ToList();

                ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customes_ID", "First_Name");
                ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                return Json(detaileslist, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveOrder(string name, int CustomerId,int Saller_ID, int Pay_ID1, int Pay_ID2, float Pay1Val, float Pay2Val, int Branch_ID,DateTime OrderDate, string X, Sales_Detailes[] order)
        {
            float finalTotal=0;
            float FinalTax =0;
            float AvgPrice = 0;
            float SalesCost = 0;

            string result = "خطأ فى انشاء الفاتورة!";
            if (name != null && order != null)
            {

                SalesInvoice model = new SalesInvoice();
                model.Sales_Date = OrderDate;
                model.Customer_ID = CustomerId;
                model.Saller_ID = Saller_ID;
                model.Pay_ID1 = Pay_ID1;
                model.Pay_ID2 = Pay_ID2; 
                model.Pay1Val = Pay1Val;
                model.Pay2Val = Pay2Val;
                model.Brn_Num = Branch_ID;
                db.SalesInvoicies.Add(model);

                foreach (var item in order)
                {
                    var orderId = Guid.NewGuid();
                    Sales_Detailes O = new Sales_Detailes();
                    O.Sales_ID = model.Sales_ID;
                    O.Product_ID = X;
                    O.Quintity = item.Quintity;
                    O.Price = item.Price;
                    O.Amount = item.Amount;
                    O.discount = item.discount;
                    O.Tax = item.Tax;
                    O.Total = item.Total;
                    finalTotal = finalTotal + O.Total;
                    FinalTax = FinalTax + O.Tax;
                    AvgPrice= db.PurchasesDetailes.Where(p=>p.Product_ID==O.Product_ID).Average(x => x.Price);
                    SalesCost =SalesCost+(AvgPrice * O.Quintity);
                    ////////////////////////////////////////////
                    //المخزون
                    AccStock Stock = new AccStock();
                    Stock.MovDate = OrderDate;
                    Stock.Quintity = item.Quintity * -1;
                    Stock.MoveType = db.Defination.FirstOrDefault().SalesAcc;
                    Stock.ProductId = Convert.ToInt32(O.Product_ID);
                    db.AccStocks.Add(Stock);
                    ////////////////////////////////////////////
                    db.SalesDetailes.Add(O);
                }
                db.SaveChanges();

                //القيود المحاسبية
                AccTransHed hed = new AccTransHed();
                AccTransDet det1 = new AccTransDet();
                AccTransDet det2 = new AccTransDet();
                AccTransDet det3 = new AccTransDet();
                AccTransDet det4 = new AccTransDet();
                AccTransDet det5 = new AccTransDet();
                AccTransDet det6 = new AccTransDet();
                int lin = Convert.ToInt32(Convert.ToInt32(db.AccTransDet.Max(x => x.tdt_lne)) + 1);
                hed.thd_num = model.Sales_ID;
                hed.thd_MovNum = model.Sales_ID;
                hed.thd_typ = db.Defination.FirstOrDefault().SalesAcc;
                hed.thd_brn = Branch_ID;
                hed.thd_dat = OrderDate;
                hed.thd_des = "فاتورة مبيعات";
                db.AccTransHead.Add(hed);
                db.SaveChanges();
                ////////////////////////////////////////////
                //1-من حساب العميل او النقدية
                if(Pay1Val>0)
                {
                    det1.thd_num = hed.thd_Key;
                    det1.tdt_num = hed.thd_num;
                    det1.tdt_lne = Convert.ToString(lin++);
                    det1.tdt_typ = db.Defination.FirstOrDefault().SalesAcc;
                    det1.tdt_brn = Branch_ID;
                    if (Pay_ID1 == 1)
                    {
                        //حساب النقدية
                        det1.tdt_L1 = db.PayModes.Where(p=>p.Pay_ID==Pay_ID1).Select(p=>p.Pay_Acc).FirstOrDefault();
                        //det1.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                    }
                    else if (Pay_ID1 != 1)
                        //حساب البنك
                        det1.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID1).Select(p => p.Pay_Acc).FirstOrDefault();
                    //det1.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det1.tdt_L2 = "0";
                    det1.tdt_C1 = "0";
                    det1.tdt_dr = Pay1Val;
                    det1.tdt_cr = 0;
                    det1.tdt_dat = OrderDate;
                    det1.tdt_des = "فاتورة مبيعات";
                    db.AccTransDet.Add(det1);
                    db.SaveChanges();
                }
                if (Pay2Val > 0)
                {
                    det6.thd_num = hed.thd_Key;
                    det6.tdt_num = hed.thd_num;
                    det6.tdt_lne = Convert.ToString(lin++);
                    det6.tdt_typ = db.Defination.FirstOrDefault().SalesAcc;
                    det6.tdt_brn = Branch_ID;
                    if (Pay_ID2 == 1)
                    {
                        //حساب النقدية
                        det6.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID2).Select(p => p.Pay_Acc).FirstOrDefault();
                        //det6.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                    }
                    else if (Pay_ID2 != 1)
                        //حساب البنك
                        det6.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID2).Select(p => p.Pay_Acc).FirstOrDefault();
                    //det6.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det6.tdt_L2 = "0";
                    det6.tdt_C1 = "0";
                    det6.tdt_dr = Pay2Val;
                    det6.tdt_cr = 0;
                    det6.tdt_dat = OrderDate;
                    det6.tdt_des = "فاتورة مبيعات";
                    db.AccTransDet.Add(det6);
                    db.SaveChanges();
                }
                //////////////////////////////////
                //2-الى حساب ايرادات المبيعات
                det2.thd_num = hed.thd_Key;
                det2.tdt_num = hed.thd_num;
                det2.tdt_lne = Convert.ToString(lin++);
                det2.tdt_typ = db.Defination.FirstOrDefault().SalesAcc;
                det2.tdt_brn = Branch_ID;
                det2.tdt_L1 = db.Defination.FirstOrDefault().AccSalesRevenues;
                det2.tdt_L2 = "0";
                det2.tdt_C1 = "0";
                det2.tdt_dr = 0;
                det2.tdt_cr = finalTotal-FinalTax;
                det2.tdt_dat = OrderDate;
                det2.tdt_des = "فاتورة مبيعات";
                db.AccTransDet.Add(det2);
                db.SaveChanges();
                //////////////////////////////////
                //3-الى حساب الضريبة
                det3.thd_num = hed.thd_Key;
                det3.tdt_num = hed.thd_num;
                det3.tdt_lne = Convert.ToString(lin++);
                det3.tdt_typ = db.Defination.FirstOrDefault().SalesAcc;
                det3.tdt_brn = Branch_ID;
                det3.tdt_L1 = db.Defination.FirstOrDefault().AccTax;
                det3.tdt_L2 = "0";
                det3.tdt_C1 = "0";
                det3.tdt_dr = 0;
                det3.tdt_cr =FinalTax;
                det3.tdt_dat = OrderDate;
                det3.tdt_des = "فاتورة مبيعات";
                db.AccTransDet.Add(det3);
                db.SaveChanges();
                //////////////////////////////////
                //4-من حساب التكلفة للمبيعات
                det4.thd_num = hed.thd_Key;
                det4.tdt_num = hed.thd_num;
                det4.tdt_lne = Convert.ToString(lin++);
                det4.tdt_typ = db.Defination.FirstOrDefault().SalesAcc;
                det4.tdt_brn = Branch_ID;
                det4.tdt_L1 = db.Defination.FirstOrDefault().AccSalesCost;
                det4.tdt_L2 = "0";
                det4.tdt_C1 = "0";
                det4.tdt_dr = SalesCost;
                det4.tdt_cr = 0;
                det4.tdt_dat = OrderDate;
                det4.tdt_des = "فاتورة مبيعات";
                db.AccTransDet.Add(det4);
                db.SaveChanges();
                //////////////////////////////////
                //5-الى حساب المخزون
                det5.thd_num = hed.thd_Key;
                det5.tdt_num = hed.thd_num;
                det5.tdt_lne = Convert.ToString(lin++);
                det5.tdt_typ = db.Defination.FirstOrDefault().SalesAcc;
                det5.tdt_brn = Branch_ID;
                det5.tdt_L1 = db.Defination.FirstOrDefault().AccInv;
                det5.tdt_L2 = "0";
                det5.tdt_C1 = "0";
                det5.tdt_dr = 0;
                det5.tdt_cr = SalesCost;
                det5.tdt_dat = OrderDate;
                det5.tdt_des = "فاتورة مبيعات";
                db.AccTransDet.Add(det5);
                db.SaveChanges();
                result = "تم اضافة الفاتورة بنجاح!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCustomer(int CustomerId)
        {

            var customer = db.Customers.Find(CustomerId);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProduct(int ProductId)
        {

            var Product = db.Products.Find(ProductId);
            return Json(Product, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteOrder(int id)
        {
            //delete invoice accounting
            var thd_num = db.AccTransHead.FirstOrDefault(b => b.thd_num == id).thd_num;
            var thd_key = db.AccTransHead.FirstOrDefault(b => b.thd_num == id).thd_Key;
            AccTransHed hed = db.AccTransHead.Find(thd_key);
            db.AccTransHead.Remove(hed);
            var det = db.AccTransDet.RemoveRange(db.AccTransDet.Where(c => c.thd_num == thd_num));

            ///////////////////////////////////////////////////////////////////
            //delete invoice
            SalesInvoice order = db.SalesInvoicies.Find(id);
            db.SalesInvoicies.Remove(order);

            ///////////////////////////////////////////////////////////////////
            //inventory affacted
            List<Sales_Detailes> salesDetalieslist = db.SalesDetailes.Where(d => d.Sales_ID == id).ToList();

            foreach (var item in salesDetalieslist)
            {
                AccStock Stock = new AccStock();
                Stock.MovDate =DateTime.Now;
                Stock.Quintity = item.Quintity;
                Stock.MoveType ="حذف فاتورة مبيعات";
                Stock.ProductId = Convert.ToInt32(item.Product_ID);
                db.AccStocks.Add(Stock);
            }
            //////////////////////////////////////////////////////////////////////////
            int val = db.SaveChanges();
            if (val > 0)
            {

                return Json(new { StatUs = 1, Message = "Product Deleted Successfly" },
                    JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index");

            }
            else
                return Json(new { StatUs = 0, Message = "Product Deleted Failed" },
                    JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteProduct(int Order_Det_Key)
        {
            //delete invoice accounting
            //var thd_num = db.AccTransDet.FirstOrDefault(b => b.tdt_Key == Order_Det_Key).thd_num;
            //var thd_key = db.AccTransHead.FirstOrDefault(b => b.thd_num == thd_num).thd_Key;
            //var det = db.AccTransDet.RemoveRange(db.AccTransDet.Where(c => c.thd_num == thd_num));
            //List<AccTransDet> DetList = db.AccTransDet.Where(a => a.thd_num == thd_num && a.tdt_Key != Order_Det_Key).ToList();

            ///////////////////////////////////////////////////////////////////////////////

            Sales_Detailes product = db.SalesDetailes.Find(Order_Det_Key);
            int orderid = product.Sales_ID;
            db.SalesDetailes.Remove(product);
            int val = db.SaveChanges();
            List<Sales_Detailes> orderlist = db.SalesDetailes.Where(x => x.Sales_ID == orderid).ToList();
            int count = db.SalesDetailes.Where(x => x.Sales_ID == orderid).Count();
            if (count <= 0)
            {
                SalesInvoice o = db.SalesInvoicies.Find(orderid);
                db.SalesInvoicies.Remove(o);
                db.SaveChanges();
            }
            if (val > 0)
            {

                return Json(new { StatUs = 1, Message = "Product Deleted Successfly" },
                    JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index");

            }
            else
                return Json(new { StatUs = 0, Message = "Product Deleted Failed" },
                    JsonRequestBehavior.AllowGet);




            //Order_Detailes order_Detailes = db.Order_Detailes.Find(id);
            //db.Order_Detailes.Remove(order_Detailes);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult UpdateProduct(int Order_Det_Key)
        {
            Sales_Detailes product = db.SalesDetailes.Find(Order_Det_Key);
            int orderid = product.Sales_ID;
            db.SalesDetailes.Remove(product);
            int val = db.SaveChanges();
            List<Sales_Detailes> orderlist = db.SalesDetailes.Where(x => x.Sales_ID == orderid).ToList();
            int count = db.SalesDetailes.Where(x => x.Sales_ID == orderid).Count();
            if (count <= 0)
            {
                SalesInvoice o = db.SalesInvoicies.Find(orderid);
                db.SalesInvoicies.Remove(o);
                db.SaveChanges();
            }
            if (val > 0)
            {

                return Json(new { StatUs = 1, Message = "Product updated Successfly" },
                    JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index");

            }
            else
                return Json(new { StatUs = 0, Message = "Product Deleted Failed" },
                    JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrderDetailesByID(int Order_Det_Key)
        {
            //System.Threading.Thread.Sleep(4000);
            Sales_Detailes orderDetailes = db.SalesDetailes.Find(Order_Det_Key);
            Product p_name = db.Products.Find(orderDetailes.Product_ID);
            ProductViewModel p = new ProductViewModel();
            p.Product_Id = orderDetailes.Product_ID;
            p.Price = orderDetailes.Price;
            p.Quintity = orderDetailes.Quintity;
            p.Tax = orderDetailes.Tax;
            p.Total = orderDetailes.Total;
            p.Amount = orderDetailes.Amount;
            p.discount = orderDetailes.discount;
            p.Order_D_key = orderDetailes.Sales_Det_Key;
            p.Product_Name = p_name.Product_Name;
            p.Order_Id = orderDetailes.Sales_ID;
            return Json(p, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveUpdateProduct(int Order_Det_Key, ProductViewModel orderDetailes)
        {
            // Quintity: Quintity, Price: Price, Amount: Amount, discount: discount, Tax: Tax, Total: Total
            Sales_Detailes order = db.SalesDetailes.Find(Order_Det_Key);
            order.Quintity = orderDetailes.Quintity;
            order.Price = orderDetailes.Price;
            order.Amount = orderDetailes.Amount;
            order.discount = orderDetailes.discount;
            order.Tax = orderDetailes.Tax;
            order.Total = orderDetailes.Total;
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            int val = db.SaveChanges();
            if (val > 0)
                return Json(new { StatUs = 1, Message = " تمت عملية التعديل بنجاح" },
                    JsonRequestBehavior.AllowGet);
            else
                return Json(new { StatUs = 0, Message = " لم تتم عملية التعديل بنجاح" },
                    JsonRequestBehavior.AllowGet);

        }

        public JsonResult CheckProductAvalible(int Product_Id)
        {
            int count = db.AccStocks.Where(x => x.ProductId == Product_Id).Sum(a => a.Quintity);
            if (count > 0)
                return Json(new { StatUs = 1,Quintiy=count},
                        JsonRequestBehavior.AllowGet);
            else
                return Json(new { StatUs = 0 },
                    JsonRequestBehavior.AllowGet);

        }


        // GET: SalesInvoices
        public ActionResult Index()
        {
            ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customes_ID", "First_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            if (db.SalesInvoicies.Count() > 0)
            {
                var x = db.SalesInvoicies.Max(o => o.Sales_ID);

                var order_Detailes = db.SalesInvoicies.Where(o => o.Sales_ID == x).Include(o => o.Sales_Detailes).Include(o => o.Customer).ToList();

                return View(order_Detailes);
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

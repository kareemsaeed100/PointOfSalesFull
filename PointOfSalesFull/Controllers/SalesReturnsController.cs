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
    public class SalesReturnsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ExportReportMax()
        {
            var x = db.SalesReturn.Max(o => o.SalesR_ID);
            List<OrderD_viewModel> ODM = db.SalesReturn.Where(o => o.SalesR_ID == x).Include(o => o.SalesReturnDetailes).Include(o => o.Customer).Select(o => new OrderD_viewModel
            {
                Email = o.Customer.Email,
                Order_Date = o.SalesR_Date.ToString(),
                First_Name = o.Customer.First_Name,
                Order_Id = o.SalesR_ID,
                Saler_Name = o.Saller.Saler_Name,
                BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
            }).ToList();
            List<ProductViewModel> detaileslist = db.SalesReturnDetailes.Where(o => o.SalesR_ID == x).Select(o => new ProductViewModel
            {
                Order_D_key = o.SalesR_Det_Key,
                Product_Name = o.product.Product_Name,
                Quintity = o.Quintity,
                Price = o.Price,
                Amount = o.Amount,
                discount = o.discount,
                Tax = o.Tax,
                Total = o.Total,
                Order_Id = o.SalesR_ID
            }).ToList();
            ReportDocument rd = new salesInvoice();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "SalesRInvoice.rpt"));
            rd.Database.Tables[0].SetDataSource(ODM.ToList());
            rd.Database.Tables[1].SetDataSource(detaileslist.ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "SalesReturnInvoice.pdf");
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
            List<OrderD_viewModel> ODM = new List<OrderD_viewModel>();
            if (db.SalesReturn.Count() > 0)
            {
                var x = db.SalesReturn.Max(o => o.SalesR_ID);
                 ODM = db.SalesReturn.Where(o => o.SalesR_ID == x).Include(o => o.SalesReturnDetailes).Include(o => o.Customer).Select(o => new OrderD_viewModel
                {
                    Email = o.Customer.Email,
                    Order_Date = o.SalesR_Date.ToString(),
                    First_Name = o.Customer.First_Name,
                    Order_Id = o.SalesR_ID,
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
            List<ProductViewModel> detaileslist = new List<ProductViewModel>();
            if (db.SalesReturn.Count() > 0)
            {
                var x = db.SalesReturn.Max(o => o.SalesR_ID);

                detaileslist = db.SalesReturnDetailes.Where(o => o.SalesR_ID == x).Select(o => new ProductViewModel
                {
                    Order_D_key = o.SalesR_Det_Key,
                    Product_Name = o.product.Product_Name,
                    Quintity = o.Quintity,
                    Price = o.Price,
                    Amount = o.Amount,
                    discount = o.discount,
                    Tax = o.Tax,
                    Total = o.Total,
                    Order_Id = o.SalesR_ID
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
            if (db.SalesReturn.Count() > 0)
            {


                List<OrderD_viewModel> ODM = db.SalesReturn.Where(o => o.SalesR_ID == order_num).Include(o => o.SalesReturnDetailes).Include(o => o.Customer).Select(o => new OrderD_viewModel
                {
                    Email = o.Customer.Email,
                    Order_Date = o.SalesR_Date.ToString(),
                    First_Name = o.Customer.First_Name,
                    Order_Id = o.SalesR_ID,
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
            if (db.SalesReturn.Count() > 0)
            {


                List<ProductViewModel> detaileslist = db.SalesReturnDetailes.Where(o => o.SalesR_ID == order_num).Select(o => new ProductViewModel
                {
                    Order_D_key = o.SalesR_Det_Key,
                    Product_Name = o.product.Product_Name,
                    Quintity = o.Quintity,
                    Price = o.Price,
                    Amount = o.Amount,
                    discount = o.discount,
                    Tax = o.Tax,
                    Total = o.Total,
                    Order_Id = o.SalesR_ID
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
        public ActionResult SaveOrder(string name,int Pay_ID1, int Pay_ID2, float Pay1Val, float Pay2Val, int Sales_Number, DateTime OrderDate, string X, SalesReturnDetailes[] order)
        {
            float finalTotal = 0;
            float FinalTax = 0;
            float AvgPrice = 0;
            float SalesCost = 0;

            string result = "Error! Order Is Not Complete!";
            if (name != null && order != null)
            {
                SalesInvoice s = db.SalesInvoicies.Find(Sales_Number);
                SalesReturn model = new SalesReturn();
                model.SalesR_Date = OrderDate;
                model.Customer_ID = s.Customer_ID;
                model.Saller_ID = s.Saller_ID;
                model.Pay_ID1 = Pay_ID1;
                model.Pay_ID2 = Pay_ID2;
                model.Pay1Val = Pay1Val;
                model.Pay2Val = Pay2Val;
                model.Brn_Num = s.Brn_Num;
                model.Sales_Number = Sales_Number;
                db.SalesReturn.Add(model);
                foreach (var item in order)
                {
                    var orderId = Guid.NewGuid();
                    SalesReturnDetailes O = new SalesReturnDetailes();
                    O.SalesR_ID = (int)model.SalesR_ID;
                    O.Product_ID = item.Product_ID;
                    O.Quintity = item.Quintity;
                    O.Price = item.Price;
                    O.Amount = item.Amount;
                    O.discount = item.discount;
                    O.Tax = item.Tax;
                    O.Total = item.Total;
                    finalTotal = finalTotal + O.Total;
                    FinalTax = FinalTax + O.Tax;
                    AvgPrice = db.PurchasesDetailes.Where(p => p.Product_ID == O.Product_ID).Average(x => x.Price);
                    SalesCost = SalesCost + (AvgPrice * O.Quintity);
                    ////////////////////////////////////////////
                    //المخزون
                    AccStock Stock = new AccStock();
                    Stock.MovDate = OrderDate;
                    Stock.Quintity = item.Quintity;
                    Stock.MoveType = db.Defination.FirstOrDefault().SalesRAcc;
                    Stock.ProductId = Convert.ToInt32(O.Product_ID);
                    db.AccStocks.Add(Stock);
                    ////////////////////////////////////////////
                    db.SalesReturnDetailes.Add(O);
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
                hed.thd_num = model.SalesR_ID;
                hed.thd_MovNum = model.SalesR_ID;
                hed.thd_typ = db.Defination.FirstOrDefault().SalesRAcc;
                hed.thd_brn = model.Brn_Num;
                hed.thd_dat = OrderDate;
                hed.thd_des = "فاتورة مرتجعات مبيعات";
                db.AccTransHead.Add(hed);
                db.SaveChanges();
                ////////////////////////////////////////////
                //////////////////////////////////
                //1-من حساب المخزون
                det1.thd_num = hed.thd_Key;
                det1.tdt_num = hed.thd_num;
                det1.tdt_lne = Convert.ToString(lin++);
                det1.tdt_typ = db.Defination.FirstOrDefault().SalesRAcc;
                det1.tdt_brn = model.Brn_Num;
                det1.tdt_L1 = db.Defination.FirstOrDefault().AccInv;
                det1.tdt_L2 = "0";
                det1.tdt_C1 = "0";
                det1.tdt_dr = SalesCost;
                det1.tdt_cr = 0;
                det1.tdt_dat = OrderDate;
                det1.tdt_des = "فاتورة مرتجعات مبيعات";
                db.AccTransDet.Add(det1);
                db.SaveChanges();
                //////////////////////////////////
                //2-الى حساب التكلفة للمبيعات
                det2.thd_num = hed.thd_Key;
                det2.tdt_num = hed.thd_num;
                det2.tdt_lne = Convert.ToString(lin++);
                det2.tdt_typ = db.Defination.FirstOrDefault().SalesRAcc;
                det2.tdt_brn = model.Brn_Num;
                det2.tdt_L1 = db.Defination.FirstOrDefault().AccSalesCost;
                det2.tdt_L2 = "0";
                det2.tdt_C1 = "0";
                det2.tdt_dr = 0;
                det2.tdt_cr = SalesCost;
                det2.tdt_dat = OrderDate;
                det2.tdt_des = "فاتورة مرتجعات مبيعات";
                db.AccTransDet.Add(det2);
                db.SaveChanges();
                //////////////////////////////////
                //3-الى حساب الضريبة
                det3.thd_num = hed.thd_Key;
                det3.tdt_num = hed.thd_num;
                det3.tdt_lne = Convert.ToString(lin++);
                det3.tdt_typ = db.Defination.FirstOrDefault().SalesRAcc;
                det3.tdt_brn = model.Brn_Num;
                det3.tdt_L1 = db.Defination.FirstOrDefault().AccTax;
                det3.tdt_L2 = "0";
                det3.tdt_C1 = "0";
                det3.tdt_dr = FinalTax;
                det3.tdt_cr = 0;
                det3.tdt_dat = OrderDate;
                det3.tdt_des = "فاتورة مرتجعات مبيعات";
                db.AccTransDet.Add(det3);
                db.SaveChanges();
                ///////////////////////////////////////////////
                //4-من حساب ايرادات المبيعات
                det4.thd_num = hed.thd_Key;
                det4.tdt_num = hed.thd_num;
                det4.tdt_lne = Convert.ToString(lin++);
                det4.tdt_typ = db.Defination.FirstOrDefault().SalesRAcc;
                det4.tdt_brn = model.Brn_Num;
                det4.tdt_L1 = db.Defination.FirstOrDefault().AccSalesRevenues;
                det4.tdt_L2 = "0";
                det4.tdt_C1 = "0";
                det4.tdt_dr = finalTotal - FinalTax;
                det4.tdt_cr = 0;
                det4.tdt_dat = OrderDate;
                det4.tdt_des = "فاتورة مرتجعات مبيعات";
                db.AccTransDet.Add(det4);
                db.SaveChanges();
                ////////////////////////////////////////////
                //5-الى حساب العميل او النقدية
                if (Pay1Val > 0)
                {
                    det5.thd_num = hed.thd_Key;
                    det5.tdt_num = hed.thd_num;
                    det5.tdt_lne = Convert.ToString(lin++);
                    det5.tdt_typ = db.Defination.FirstOrDefault().SalesRAcc;
                    det5.tdt_brn = model.Brn_Num;
                    if (Pay_ID1 == 1)
                    {
                        //حساب النقدية
                        //det5.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                        det5.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID1).Select(p => p.Pay_Acc).FirstOrDefault();

                    }
                    else if (Pay_ID1 != 1)
                        //حساب البنك
                       // det5.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det5.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID1).Select(p => p.Pay_Acc).FirstOrDefault();

                    det5.tdt_L2 = "0";
                    det5.tdt_C1 = "0";
                    det5.tdt_dr = 0;
                    det5.tdt_cr = Pay1Val;
                    det5.tdt_dat = OrderDate;
                    det5.tdt_des = "فاتورة مرتجعات مبيعات";
                    db.AccTransDet.Add(det5);
                    db.SaveChanges();
                }
                if (Pay2Val > 0)
                {
                    det6.thd_num = hed.thd_Key;
                    det6.tdt_num = hed.thd_num;
                    det6.tdt_lne = Convert.ToString(lin++);
                    det6.tdt_typ = db.Defination.FirstOrDefault().SalesRAcc;
                    det6.tdt_brn = model.Brn_Num;
                    if (Pay_ID2 == 1)
                    {
                        //حساب النقدية
                       // det6.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                        det6.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID2).Select(p => p.Pay_Acc).FirstOrDefault();

                    }
                    else if (Pay_ID2 != 1)
                        //حساب البنك
                        //det6.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det6.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID2).Select(p => p.Pay_Acc).FirstOrDefault();

                    det6.tdt_L2 = "0";
                    det6.tdt_C1 = "0";
                    det6.tdt_dr = 0;
                    det6.tdt_cr = Pay2Val;
                    det6.tdt_dat = OrderDate;
                    det6.tdt_des = "فاتورة مرتجعات مبيعات";
                    db.AccTransDet.Add(det6);
                    db.SaveChanges();
                }
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

            SalesReturn order = db.SalesReturn.Find(id);
            db.SalesReturn.Remove(order);
            ///////////////////////////////////////////////////////////////////
            //inventory affacted
            List<SalesReturnDetailes> salesRDetalieslist = db.SalesReturnDetailes.Where(d => d.SalesR_ID == id).ToList();

            foreach (var item in salesRDetalieslist)
            {
                AccStock Stock = new AccStock();
                Stock.MovDate = DateTime.Now;
                Stock.Quintity = item.Quintity-1;
                Stock.MoveType = "حذف فاتورة مرتجع مبيعات";
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
            SalesReturnDetailes product = db.SalesReturnDetailes.Find(Order_Det_Key);
            int orderid = product.SalesR_ID;
            db.SalesReturnDetailes.Remove(product);
            int val = db.SaveChanges();
            List<SalesReturnDetailes> orderlist = db.SalesReturnDetailes.Where(x => x.SalesR_ID == orderid).ToList();
            int count = db.SalesReturnDetailes.Where(x => x.SalesR_ID == orderid).Count();
            if (count <= 0)
            {
                SalesReturn o = db.SalesReturn.Find(orderid);
                db.SalesReturn.Remove(o);
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
            SalesReturnDetailes product = db.SalesReturnDetailes.Find(Order_Det_Key);
            int orderid = product.SalesR_ID;
            db.SalesReturnDetailes.Remove(product);
            int val = db.SaveChanges();
            List<SalesReturnDetailes> orderlist = db.SalesReturnDetailes.Where(x => x.SalesR_ID == orderid).ToList();
            int count = db.SalesReturnDetailes.Where(x => x.SalesR_ID == orderid).Count();
            if (count <= 0)
            {
                SalesReturn o = db.SalesReturn.Find(orderid);
                db.SalesReturn.Remove(o);
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

        public JsonResult GetOrderDetailesByID(int Order_Det_Key)
        {
            //System.Threading.Thread.Sleep(4000);
            SalesReturnDetailes orderDetailes = db.SalesReturnDetailes.Find(Order_Det_Key);
            Product p_name = db.Products.Find(orderDetailes.Product_ID);
            ProductViewModel p = new ProductViewModel();
            p.Price = orderDetailes.Price;
            p.Quintity = orderDetailes.Quintity;
            p.Tax = orderDetailes.Tax;
            p.Total = orderDetailes.Total;
            p.Amount = orderDetailes.Amount;
            p.discount = orderDetailes.discount;
            p.Order_D_key = orderDetailes.SalesR_Det_Key;
            p.Product_Name = p_name.Product_Name;
            p.Order_Id = orderDetailes.SalesR_ID;
            return Json(p, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveUpdateProduct(int Order_Det_Key, ProductViewModel orderDetailes)
        {
            // Quintity: Quintity, Price: Price, Amount: Amount, discount: discount, Tax: Tax, Total: Total
            SalesReturnDetailes order = db.SalesReturnDetailes.Find(Order_Det_Key);
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

        // GET: SalesReturns
        public ActionResult Index()
        {
            ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customes_ID", "First_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            if (db.SalesReturn.Count() > 0)
            {
                var x = db.SalesReturn.Max(o => o.SalesR_ID);

                var order_Detailes = db.SalesReturn.Where(o => o.SalesR_ID == x).Include(o => o.SalesReturnDetailes).Include(o => o.Customer).ToList();

                return View(order_Detailes);
            }
            return View("index");
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

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
        public class PurchasesInvoicesController : Controller
        {
            private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ExportReportMax()
        {
            var x = db.PurchasesInvoices.Max(o => o.Pure_ID);
            List<OrderD_viewModel> ODM = db.PurchasesInvoices.Where(o => o.Pure_ID == x).Include(o => o.Purchases_Detailes).Include(o => o.Supplier).Select(o => new OrderD_viewModel
            {
                Email = o.Supplier.Sub_Email,
                Order_Date = o.Pure_Date.ToString(),
                First_Name = o.Supplier.Sub_Name,
                Order_Id = o.Pure_ID,
                Saler_Name = o.Saller.Saler_Name,
                BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
            }).ToList();
            List<ProductViewModel> detaileslist = db.PurchasesDetailes.Where(o => o.Pure_ID == x).Select(o => new ProductViewModel
            {
                Order_D_key = o.Pure_Det_Key,
                Product_Name = o.product.Product_Name,
                Quintity = o.Quintity,
                Price = o.Price,
                Amount = o.Amount,
                discount = o.discount,
                Tax = o.Tax,
                Total = o.Total,
                Order_Id = o.Pure_ID
            }).ToList();
            ReportDocument rd = new PureInvoice();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "PureInvoice.rpt"));
            rd.Database.Tables[0].SetDataSource(ODM);
            rd.Database.Tables[1].SetDataSource(detaileslist);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "PureInvoice.pdf");
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult ExportReportSearch(int Pure_num)
        {

            List<OrderD_viewModel> ODM = db.PurchasesInvoices.Where(o => o.Pure_ID == Pure_num).Include(o => o.Purchases_Detailes).Include(o => o.Supplier).Select(o => new OrderD_viewModel
            {
                Email = o.Supplier.Sub_Email,
                Order_Date = o.Pure_Date.ToString(),
                First_Name = o.Supplier.Sub_Name,
                Order_Id = o.Pure_ID,
                Saler_Name = o.Saller.Saler_Name,
                BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
            }).ToList();
            List<ProductViewModel> detaileslist = db.PurchasesDetailes.Where(o => o.Pure_ID == Pure_num).Select(o => new ProductViewModel
            {
                Order_D_key = o.Pure_Det_Key,
                Product_Name = o.product.Product_Name,
                Quintity = o.Quintity,
                Price = o.Price,
                Amount = o.Amount,
                discount = o.discount,
                Tax = o.Tax,
                Total = o.Total,
                Order_Id = o.Pure_ID
            }).ToList();
            ReportDocument rd = new PureInvoice();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "PureInvoice.rpt"));
            rd.Database.Tables[0].SetDataSource(ODM);
            rd.Database.Tables[1].SetDataSource(detaileslist);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "PureInvoice.pdf");
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult Getorders()
            {
                if (db.PurchasesInvoices.Count() > 0)
                {
                    var x = db.PurchasesInvoices.Max(o => o.Pure_ID);
                    List<OrderD_viewModel> ODM = db.PurchasesInvoices.Where(o => o.Pure_ID == x).Include(o => o.Purchases_Detailes).Include(o => o.Supplier).Select(o => new OrderD_viewModel
                    {
                        Email = o.Supplier.Sub_Email,
                        Order_Date = o.Pure_Date.ToString(),
                        First_Name = o.Supplier.Sub_Name,
                        Order_Id = o.Pure_ID,
                        Saler_Name = o.Saller.Saler_Name,
                        BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                        PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
                    }).ToList();

                    ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                    ViewBag.Supplier_ID = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
                    ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                    ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                    ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                    return Json(ODM, JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            public JsonResult GetorderDetailess()
            {
                if (db.PurchasesInvoices.Count() > 0)
                {
                    var x = db.PurchasesInvoices.Max(o => o.Pure_ID);

                    List<ProductViewModel> detaileslist = db.PurchasesDetailes.Where(o => o.Pure_ID == x).Select(o => new ProductViewModel
                    {
                        Order_D_key = o.Pure_Det_Key,
                        Product_Name = o.product.Product_Name,
                        Quintity = o.Quintity,
                        Price = o.Price,
                        Amount = o.Amount,
                        discount = o.discount,
                        Tax = o.Tax,
                        Total = o.Total,
                        Order_Id = o.Pure_ID
                    }).ToList();

                    ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                   ViewBag.Supplier_ID = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
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
                if (db.PurchasesInvoices.Count() > 0)
                {


                    List<OrderD_viewModel> ODM = db.PurchasesInvoices.Where(o => o.Pure_ID == order_num).Include(o => o.Purchases_Detailes).Include(o => o.Supplier).Select(o => new OrderD_viewModel
                    {
                        Email = o.Supplier.Sub_Email,
                        Order_Date = o.Pure_Date.ToString(),
                        First_Name = o.Supplier.Sub_Name,
                        Order_Id = o.Pure_ID,
                        Saler_Name = o.Saller.Saler_Name,
                        BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                        PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
                    }).ToList();

                    ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                    ViewBag.Supplier_ID = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
                   ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                    ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                    ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                    return Json(ODM, JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            public JsonResult GetorderDetailesSearch(int order_num)
            {
                if (db.PurchasesInvoices.Count() > 0)
                {


                    List<ProductViewModel> detaileslist = db.PurchasesDetailes.Where(o => o.Pure_ID == order_num).Select(o => new ProductViewModel
                    {
                        Order_D_key = o.Pure_Det_Key,
                        Product_Name = o.product.Product_Name,
                        Quintity = o.Quintity,
                        Price = o.Price,
                        Amount = o.Amount,
                        discount = o.discount,
                        Tax = o.Tax,
                        Total = o.Total,
                        Order_Id = o.Pure_ID,
                        Product_Id = o.Product_ID
                    }).ToList();

                    ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                   ViewBag.Supplier_ID = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
                   ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                    ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                    ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                    return Json(detaileslist, JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            [HttpPost]
            public ActionResult SaveOrder(string name, int SupplierId, int Saller_ID, int Pay_ID1, int Pay_ID2, float Pay1Val, float Pay2Val, int Branch_ID, DateTime OrderDate, string X, PurchasesDetailes[] order)
            {
            float finalTotal = 0;
            float FinalTax = 0;
            string result = "Error! Order Is Not Complete!";
                if (name != null && order != null)
                {

                PurchasesInvoice model = new PurchasesInvoice();
                    model.Pure_Date =OrderDate;
                    model.Sub_ID = SupplierId;
                    model.Saller_ID = Saller_ID;
                    model.Pay_ID1 = Pay_ID1;
                    model.Brn_Num = Branch_ID;
                    db.PurchasesInvoices.Add(model);

                    foreach (var item in order)
                    {
                        var orderId = Guid.NewGuid();
                        PurchasesDetailes O = new PurchasesDetailes();
                        O.Pure_ID = model.Pure_ID;
                        O.Product_ID = X;
                        O.Quintity = item.Quintity;
                        O.Price = item.Price;
                        O.Amount = item.Amount;
                        O.discount = item.discount;
                        O.Tax = item.Tax;
                        O.Total = item.Total;
                        finalTotal = finalTotal + O.Total;
                        FinalTax = FinalTax + O.Tax;
                    db.PurchasesDetailes.Add(O);
                    ////////////////////////////////////////////
                    //المخزون
                    AccStock Stock = new AccStock();
                    Stock.MovDate = OrderDate;
                    Stock.Quintity = item.Quintity;
                    Stock.MoveType = db.Defination.FirstOrDefault().PureAcc;
                    Stock.ProductId = Convert.ToInt32(O.Product_ID);
                    db.AccStocks.Add(Stock);
                    ////////////////////////////////////////////
                  
                    }
                    db.SaveChanges();
                //القيود المحاسبية
                AccTransHed hed = new AccTransHed();
                AccTransDet det1 = new AccTransDet();
                AccTransDet det2 = new AccTransDet();
                AccTransDet det3 = new AccTransDet();
                AccTransDet det4= new AccTransDet();
                AccTransDet det5 = new AccTransDet();
                int lin = Convert.ToInt32(Convert.ToInt32(db.AccTransDet.Max(x => x.tdt_lne)) + 1);
                hed.thd_num = model.Pure_ID;
                hed.thd_MovNum = model.Pure_ID;
                hed.thd_typ = db.Defination.FirstOrDefault().PureAcc;
                hed.thd_brn = Branch_ID;
                hed.thd_dat = OrderDate;
                hed.thd_des = "فاتورة مشتريات";
                db.AccTransHead.Add(hed);
                db.SaveChanges();
                ////////////////////////////////////////////
                //////////////////////////////////
                //1-من حساب المخزون
                det1.thd_num = hed.thd_Key;
                det1.tdt_num = hed.thd_num;
                det1.tdt_lne = Convert.ToString(lin++);
                det1.tdt_typ = db.Defination.FirstOrDefault().PureAcc;
                det1.tdt_brn = model.Brn_Num;
                det1.tdt_L1 = db.Defination.FirstOrDefault().AccInv;
                det1.tdt_L2 = "0";
                det1.tdt_C1 = "0";
                det1.tdt_dr = finalTotal-FinalTax;
                det1.tdt_cr = 0;
                det1.tdt_dat = OrderDate;
                det1.tdt_des = "فاتورةمشتريات";
                db.AccTransDet.Add(det1);
                db.SaveChanges();
                //////////////////////////////////
                //////////////////////////////////
                //2-من حساب الضريبة
                det2.thd_num = hed.thd_Key;
                det2.tdt_num = hed.thd_num;
                det2.tdt_lne = Convert.ToString(lin++);
                det2.tdt_typ = db.Defination.FirstOrDefault().PureAcc;
                det2.tdt_brn = model.Brn_Num;
                det2.tdt_L1 = db.Defination.FirstOrDefault().AccTax;
                det2.tdt_L2 = "0";
                det2.tdt_C1 = "0";
                det2.tdt_dr = FinalTax;
                det2.tdt_cr = 0;
                det2.tdt_dat = OrderDate;
                det2.tdt_des = "فاتورةمشتريات";
                db.AccTransDet.Add(det2);
                db.SaveChanges();
                ///////////////////////////////////////////////
                ////////////////////////////////////////////
                //3-الى حساب النقدية او البنك 
                if (Pay1Val > 0)
                {
                    det3.thd_num = hed.thd_Key;
                    det3.tdt_num = hed.thd_num;
                    det3.tdt_lne = Convert.ToString(lin++);
                    det3.tdt_typ = db.Defination.FirstOrDefault().PureAcc;
                    det3.tdt_brn = model.Brn_Num;
                    if (Pay_ID1 == 1)
                    {
                        //حساب النقدية
                        //det3.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                        det3.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID1).Select(p => p.Pay_Acc).FirstOrDefault();

                    }
                    else if (Pay_ID1 != 1)
                        //حساب البنك
                        //det3.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                        det3.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID1).Select(p => p.Pay_Acc).FirstOrDefault();

                    det3.tdt_L2 = "0";
                    det3.tdt_C1 = "0";
                    det3.tdt_dr = 0;
                    det3.tdt_cr = Pay1Val;
                    det3.tdt_dat = OrderDate;
                    det3.tdt_des = "فاتورةمشتريات";
                    db.AccTransDet.Add(det3);
                    db.SaveChanges();
                }
                if (Pay2Val > 0)
                {
                    det4.thd_num = hed.thd_Key;
                    det4.tdt_num = hed.thd_num;
                    det4.tdt_lne = Convert.ToString(lin++);
                    det4.tdt_typ = db.Defination.FirstOrDefault().PureAcc;
                    det4.tdt_brn = model.Brn_Num;
                    if (Pay_ID2 == 1)
                    {
                        //حساب النقدية
                       // det4.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                          det4.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID2).Select(p => p.Pay_Acc).FirstOrDefault();

                    }
                    else if (Pay_ID2 != 1)
                        //حساب البنك
                       // det4.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det4.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID2).Select(p => p.Pay_Acc).FirstOrDefault();

                    det4.tdt_L2 = "0";
                    det4.tdt_C1 = "0";
                    det4.tdt_dr = 0;
                    det4.tdt_cr = Pay2Val;
                    det4.tdt_dat = OrderDate;
                    det4.tdt_des = "فاتورة مشتريات";
                    db.AccTransDet.Add(det4);
                    db.SaveChanges();
                }
                ////////////////////////////////////////////
                //3-الى حساب المورد 
                if (finalTotal>(Pay1Val+Pay2Val))
                {
                    
                    det5.thd_num = hed.thd_Key;
                    det5.tdt_num = hed.thd_num;
                    det5.tdt_lne = Convert.ToString(lin++);
                    det5.tdt_typ = db.Defination.FirstOrDefault().PureAcc;
                    det5.tdt_brn = Branch_ID;
                    det5.tdt_L1 = db.Defination.FirstOrDefault().AccSublier;
                    det5.tdt_L2 = "0";
                    det5.tdt_C1 = SupplierId.ToString();
                    det5.tdt_dr = 0;
                    det5.tdt_cr = finalTotal -( Pay2Val+Pay1Val);
                    det5.tdt_dat = OrderDate;
                    det5.tdt_des = "فاتورة مشتريات";
                    db.AccTransDet.Add(det5);
                    db.SaveChanges();
                }
                    result = "تم اضافة الفاتورة بنجاح!";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            [HttpGet]
            public JsonResult GetSupplier(int SupplierId)
            {

                var Supplier = db.Supplier.Find(SupplierId);
                return Json(Supplier, JsonRequestBehavior.AllowGet);
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
            PurchasesInvoice order = db.PurchasesInvoices.Find(id);
                db.PurchasesInvoices.Remove(order);
            ///////////////////////////////////////////////////////////////////
            //inventory affacted
            List<PurchasesDetailes> PurchasesDetailes = db.PurchasesDetailes.Where(d => d.Pure_ID == id).ToList();

            foreach (var item in PurchasesDetailes)
            {
                AccStock Stock = new AccStock();
                Stock.MovDate = DateTime.Now;
                Stock.Quintity = item.Quintity*-1;
                Stock.MoveType = "حذف فاتورة مشتريات";
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
                PurchasesDetailes product = db.PurchasesDetailes.Find(Order_Det_Key);
                int orderid = product.Pure_ID;
                db.PurchasesDetailes.Remove(product);
                int val = db.SaveChanges();
                List<PurchasesDetailes> orderlist = db.PurchasesDetailes.Where(x => x.Pure_ID == orderid).ToList();
                int count = db.PurchasesDetailes.Where(x => x.Pure_ID == orderid).Count();
                if (count <= 0)
                {
                    PurchasesInvoice o = db.PurchasesInvoices.Find(orderid);
                    db.PurchasesInvoices.Remove(o);
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
                PurchasesDetailes product = db.PurchasesDetailes.Find(Order_Det_Key);
                int orderid = product.Pure_ID;
                db.PurchasesDetailes.Remove(product);
                int val = db.SaveChanges();
                List<PurchasesDetailes> orderlist = db.PurchasesDetailes.Where(x => x.Pure_ID == orderid).ToList();
                int count = db.PurchasesDetailes.Where(x => x.Pure_ID == orderid).Count();
                if (count <= 0)
                {
                PurchasesInvoice o = db.PurchasesInvoices.Find(orderid);
                    db.PurchasesInvoices.Remove(o);
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
                PurchasesDetailes orderDetailes = db.PurchasesDetailes.Find(Order_Det_Key);
                Product p_name = db.Products.Find(orderDetailes.Product_ID);
                ProductViewModel p = new ProductViewModel();
                p.Product_Id = orderDetailes.Product_ID;
                p.Price = orderDetailes.Price;
                p.Quintity = orderDetailes.Quintity;
                p.Tax = orderDetailes.Tax;
                p.Total = orderDetailes.Total;
                p.Amount = orderDetailes.Amount;
                p.discount = orderDetailes.discount;
                p.Order_D_key = orderDetailes.Pure_Det_Key;
                p.Product_Name = p_name.Product_Name;
                p.Order_Id = orderDetailes.Pure_ID;
                return Json(p, JsonRequestBehavior.AllowGet);

            }


            public JsonResult SaveUpdateProduct(int Order_Det_Key, ProductViewModel orderDetailes)
            {
            // Quintity: Quintity, Price: Price, Amount: Amount, discount: discount, Tax: Tax, Total: Total
               PurchasesDetailes order = db.PurchasesDetailes.Find(Order_Det_Key);
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

            // GET: PurchasesInvoicess
            public ActionResult Index()
            {
                ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
                ViewBag.Supplier_ID = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
                 ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                if (db.PurchasesInvoices.Count() > 0)
                {
                    var x = db.PurchasesInvoices.Max(o => o.Pure_ID);

                    var order_Detailes = db.PurchasesInvoices.Where(o => o.Pure_ID == x).Include(o => o.Purchases_Detailes).Include(o => o.Supplier).ToList();

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

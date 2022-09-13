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
    public class PurchasesReturnsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ExportReportMax()
        {
            var x = db.PurchasesReturn.Max(o => o.PureR_ID);
            List<OrderD_viewModel> ODM = db.PurchasesReturn.Where(o => o.PureR_ID == x).Include(o => o.PurchasesReturnDetailes).Include(o => o.Supplier).Select(o => new OrderD_viewModel
            {
                Email = o.Supplier.Sub_Email,
                Order_Date = o.PureR_Date.ToString(),
                First_Name = o.Supplier.Sub_Name,
                Order_Id = o.PureR_ID,
                Saler_Name = o.Saller.Saler_Name,
                BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
            }).ToList();
            List<ProductViewModel> detaileslist = db.PurchasesReturnDetailes.Where(o => o.PureR_ID == x).Select(o => new ProductViewModel
            {
                Order_D_key = o.PureR_Det_Key,
                Product_Name = o.product.Product_Name,
                Quintity = o.Quintity,
                Price = o.Price,
                Amount = o.Amount,
                discount = o.discount,
                Tax = o.Tax,
                Total = o.Total,
                Order_Id = o.PureR_ID
            }).ToList();
            ReportDocument rd = new PureRInvoice();
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

            List<OrderD_viewModel> ODM = db.PurchasesReturn.Where(o => o.PureR_ID == Pure_num).Include(o => o.PurchasesReturnDetailes).Include(o => o.Supplier).Select(o => new OrderD_viewModel
            {
                Email = o.Supplier.Sub_Email,
                Order_Date = o.PureR_Date.ToString(),
                First_Name = o.Supplier.Sub_Name,
                Order_Id = o.PureR_ID,
                Saler_Name = o.Saller.Saler_Name,
                BranchName = db.Branchies.FirstOrDefault(b => b.Branch_ID == o.Brn_Num).Branch_Name.ToString(),
                PayName = db.PayModes.FirstOrDefault(b => b.Pay_ID == o.Brn_Num).Pay_Name.ToString()
            }).ToList();
            List<ProductViewModel> detaileslist = db.PurchasesReturnDetailes.Where(o => o.PureR_ID == Pure_num).Select(o => new ProductViewModel
            {
                Order_D_key = o.PureR_Det_Key,
                Product_Name = o.product.Product_Name,
                Quintity = o.Quintity,
                Price = o.Price,
                Amount = o.Amount,
                discount = o.discount,
                Tax = o.Tax,
                Total = o.Total,
                Order_Id = o.PureR_ID
            }).ToList();
            ReportDocument rd = new PureRInvoice();
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
            List<OrderD_viewModel> ODM = new List<OrderD_viewModel>();
            if (db.PurchasesReturn.Count() > 0)
            {
                var x = db.PurchasesReturn.Max(o => o.PureR_ID);
                ODM = db.PurchasesReturn.Where(o => o.PureR_ID == x).Include(o => o.PurchasesReturnDetailes).Include(o => o.Supplier).Select(o => new OrderD_viewModel
                {
                    Email = o.Supplier.Sub_Email,
                    Order_Date = o.PureR_Date.ToString(),
                    First_Name = o.Supplier.Sub_Name,
                    Order_Id = o.PureR_ID,
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
            List<ProductViewModel> detaileslist = new List<ProductViewModel>();
            if (db.PurchasesReturn.Count() > 0)
            {
                var x = db.PurchasesReturn.Max(o => o.PureR_ID);

                detaileslist = db.PurchasesReturnDetailes.Where(o => o.PureR_ID == x).Select(o => new ProductViewModel
                {
                    Order_D_key = o.PureR_Det_Key,
                    Product_Name = o.product.Product_Name,
                    Quintity = o.Quintity,
                    Price = o.Price,
                    Amount = o.Amount,
                    discount = o.discount,
                    Tax = o.Tax,
                    Total = o.Total,
                    Order_Id = o.PureR_ID
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
            if (db.PurchasesReturn.Count() > 0)
            {


                List<OrderD_viewModel> ODM = db.PurchasesReturn.Where(o => o.PureR_ID == order_num).Include(o => o.PurchasesReturnDetailes).Include(o => o.Supplier).Select(o => new OrderD_viewModel
                {
                    Email = o.Supplier.Sub_Email,
                    Order_Date = o.PureR_Date.ToString(),
                    First_Name = o.Supplier.Sub_Name,
                    Order_Id = o.PureR_ID,
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
            if (db.PurchasesReturn.Count() > 0)
            {


                List<ProductViewModel> detaileslist = db.PurchasesReturnDetailes.Where(o => o.PureR_ID == order_num).Select(o => new ProductViewModel
                {
                    Order_D_key = o.PureR_Det_Key,
                    Product_Name = o.product.Product_Name,
                    Quintity = o.Quintity,
                    Price = o.Price,
                    Amount = o.Amount,
                    discount = o.discount,
                    Tax = o.Tax,
                    Total = o.Total,
                    Order_Id = o.PureR_ID
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
        public ActionResult SaveOrder(string name,int Pay_ID1, int Pay_ID2, float Pay1Val, float Pay2Val, int Sales_Number, DateTime OrderDate, string X, PurchasesReturnDetailes[] order)
        {
            float finalTotal = 0;
            float FinalTax = 0;
            string result = "Error! Order Is Not Complete!";
            if (name != null && order != null)
            {
                PurchasesInvoice s = db.PurchasesInvoices.Find(Sales_Number);
                PurchasesReturn model = new PurchasesReturn();
                model.PureR_Date =OrderDate;
                model.Sub_ID = s.Sub_ID;
                model.Saller_ID = s.Saller_ID;
                model.Pay_ID1 = Pay_ID1;
                model.Brn_Num = s.Brn_Num;
                model.Pure_Number = Sales_Number;
                db.PurchasesReturn.Add(model);
                foreach (var item in order)
                {
                    var orderId = Guid.NewGuid();
                    PurchasesReturnDetailes O = new PurchasesReturnDetailes();
                    O.PureR_ID = (int)model.PureR_ID;
                    O.Product_ID = item.Product_ID;
                    O.Quintity = item.Quintity;
                    O.Price = item.Price;
                    O.Amount = item.Amount;
                    O.discount = item.discount;
                    O.Tax = item.Tax;
                    O.Total = item.Total;
                    db.PurchasesReturnDetailes.Add(O);
                    finalTotal = finalTotal + O.Total;
                    FinalTax = FinalTax + O.Tax;
                    ////////////////////////////////////////////
                    //المخزون
                    AccStock Stock = new AccStock();
                    Stock.MovDate = OrderDate;
                    Stock.Quintity = item.Quintity*-1;
                    Stock.MoveType = db.Defination.FirstOrDefault().PureRAcc;
                    Stock.ProductId = Convert.ToInt32(O.Product_ID);
                    db.AccStocks.Add(Stock);
                    ////////////////////////////////////////////
                    //القيود المحاسبية
                    AccTransHed hed = new AccTransHed();
                    AccTransDet det1 = new AccTransDet();
                    AccTransDet det2 = new AccTransDet();
                    AccTransDet det3 = new AccTransDet();
                    AccTransDet det4 = new AccTransDet();
                    AccTransDet det5 = new AccTransDet();
                    int lin = Convert.ToInt32(Convert.ToInt32(db.AccTransDet.Max(x => x.tdt_lne)) + 1);
                    hed.thd_num = model.PureR_ID;
                    hed.thd_MovNum = model.PureR_ID;
                    hed.thd_typ = db.Defination.FirstOrDefault().PureRAcc;
                    hed.thd_brn = model.Brn_Num;
                    hed.thd_dat = OrderDate;
                    hed.thd_des = "فاتورةمرتجعات مشتريات";
                    db.AccTransHead.Add(hed);
                    db.SaveChanges();
                    ////////////////////////////////////////////
                    ////////////////////////////////////////////
                    //1-من حساب النقدية او البنك 
                    if (Pay1Val > 0)
                    {
                        det1.thd_num = hed.thd_Key;
                        det1.tdt_num = hed.thd_num;
                        det1.tdt_lne = Convert.ToString(lin++);
                        det1.tdt_typ = db.Defination.FirstOrDefault().PureRAcc;
                        det1.tdt_brn = model.Brn_Num;
                        if (Pay_ID1 == 1)
                        {
                            //حساب النقدية
                            //det1.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                            det1.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID1).Select(p => p.Pay_Acc).FirstOrDefault();

                        }
                        else if (Pay_ID1 != 1)
                            //حساب البنك
                            //det1.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                            det1.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID1).Select(p => p.Pay_Acc).FirstOrDefault();

                        det1.tdt_L2 = "0";
                        det1.tdt_C1 = "0";
                        det1.tdt_dr = Pay1Val;
                        det1.tdt_cr = 0;
                        det1.tdt_dat = OrderDate;
                        det1.tdt_des = "فاتورةمرتجعات مشتريات";
                        db.AccTransDet.Add(det1);
                        db.SaveChanges();
                    }
                    if (Pay2Val > 0)
                    {
                        det2.thd_num = hed.thd_Key;
                        det2.tdt_num = hed.thd_num;
                        det2.tdt_lne = Convert.ToString(lin++);
                        det2.tdt_typ = db.Defination.FirstOrDefault().PureRAcc;
                        det2.tdt_brn = model.Brn_Num;
                        if (Pay_ID2 == 1)
                        {
                            //حساب النقدية
                            //  det2.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                             det2.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID2).Select(p => p.Pay_Acc).FirstOrDefault();

                        }
                        else if (Pay_ID2 != 1)
                            //حساب البنك
                            //det2.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                            det2.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == Pay_ID2).Select(p => p.Pay_Acc).FirstOrDefault();

                        det2.tdt_L2 = "0";
                        det2.tdt_C1 = model.Sub_ID.ToString();
                        det2.tdt_dr = Pay2Val ;
                        det2.tdt_cr = 0;
                        det2.tdt_dat = OrderDate;
                        det2.tdt_des = "فاتورة مرتجعات مبيعات";
                        db.AccTransDet.Add(det2);
                        db.SaveChanges();
                    }
                    ////////////////////////////////////////////
                    //3-من حساب المورد 
                    if (finalTotal > (Pay1Val + Pay2Val))
                    {

                        det3.thd_num = hed.thd_Key;
                        det3.tdt_num = hed.thd_num;
                        det3.tdt_lne = Convert.ToString(lin++);
                        det3.tdt_typ = db.Defination.FirstOrDefault().PureRAcc;
                        det3.tdt_brn = model.Brn_Num;
                        det3.tdt_L1 = db.Defination.FirstOrDefault().AccSublier;
                        det3.tdt_L2 = "0";
                        det3.tdt_C1 = model.Sub_ID.ToString();
                        det3.tdt_dr = finalTotal - (Pay2Val + Pay1Val);
                        det3.tdt_cr = 0;
                        det3.tdt_dat = OrderDate;
                        det3.tdt_des = "فاتورة مرتجعات مبيعات";
                        db.AccTransDet.Add(det3);
                        db.SaveChanges();
                    }
                    ////////////////////////////////////////////
                    //////////////////////////////////
                    //4-الى حساب المخزون
                    det4.thd_num = hed.thd_Key;
                    det4.tdt_num = hed.thd_num;
                    det4.tdt_lne = Convert.ToString(lin++);
                    det4.tdt_typ = db.Defination.FirstOrDefault().PureRAcc;
                    det4.tdt_brn = model.Brn_Num;
                    det4.tdt_L1 = db.Defination.FirstOrDefault().AccInv;
                    det4.tdt_L2 = "0";
                    det4.tdt_C1 = "0";
                    det4.tdt_dr = 0;
                    det4.tdt_cr = finalTotal - FinalTax ;
                    det4.tdt_dat = OrderDate;
                    det4.tdt_des = "فاتورة مرتجعات مبيعات";
                    db.AccTransDet.Add(det4);
                    db.SaveChanges();
                    //////////////////////////////////
                    //////////////////////////////////
                    //5-الى حساب الضريبة
                    det5.thd_num = hed.thd_Key;
                    det5.tdt_num = hed.thd_num; ;
                    det5.tdt_lne = Convert.ToString(lin++);
                    det5.tdt_typ = db.Defination.FirstOrDefault().PureRAcc;
                    det5.tdt_brn = model.Brn_Num;
                    det5.tdt_L1 = db.Defination.FirstOrDefault().AccTax;
                    det5.tdt_L2 = "0";
                    det5.tdt_C1 = "0";
                    det5.tdt_dr =0 ;
                    det5.tdt_cr =FinalTax ;
                    det5.tdt_dat = OrderDate;
                    det5.tdt_des = "فاتورة مرتجعات مبيعات";
                    db.AccTransDet.Add(det5);
                    db.SaveChanges();
                    ///////////////////////////////////////////////
                }
                db.SaveChanges();
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

            PurchasesReturn order = db.PurchasesReturn.Find(id);
            db.PurchasesReturn.Remove(order);
            ///////////////////////////////////////////////////////////////////
            //inventory affacted
            List<PurchasesReturnDetailes> PureRDetalieslist = db.PurchasesReturnDetailes.Where(d => d.PureR_ID == id).ToList();

            foreach (var item in PureRDetalieslist)
            {
                AccStock Stock = new AccStock();
                Stock.MovDate = DateTime.Now;
                Stock.Quintity = item.Quintity * -1;
                Stock.MoveType = "حذف فاتورة مرتجع مشتريات";
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
            PurchasesReturnDetailes product = db.PurchasesReturnDetailes.Find(Order_Det_Key);
            int orderid = product.PureR_ID;
            db.PurchasesReturnDetailes.Remove(product);
            int val = db.SaveChanges();
            List<PurchasesReturnDetailes> orderlist = db.PurchasesReturnDetailes.Where(x => x.PureR_ID == orderid).ToList();
            int count = db.PurchasesReturnDetailes.Where(x => x.PureR_ID == orderid).Count();
            if (count <= 0)
            {
                PurchasesReturn o = db.PurchasesReturn.Find(orderid);
                db.PurchasesReturn.Remove(o);
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
            PurchasesReturnDetailes product = db.PurchasesReturnDetailes.Find(Order_Det_Key);
            int orderid = product.PureR_ID;
            db.PurchasesReturnDetailes.Remove(product);
            int val = db.SaveChanges();
            List<PurchasesReturnDetailes> orderlist = db.PurchasesReturnDetailes.Where(x => x.PureR_ID == orderid).ToList();
            int count = db.PurchasesReturnDetailes.Where(x => x.PureR_ID == orderid).Count();
            if (count <= 0)
            {
                PurchasesReturn o = db.PurchasesReturn.Find(orderid);
                db.PurchasesReturn.Remove(o);
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
            PurchasesReturnDetailes orderDetailes = db.PurchasesReturnDetailes.Find(Order_Det_Key);
            Product p_name = db.Products.Find(orderDetailes.Product_ID);
            ProductViewModel p = new ProductViewModel();
            p.Price = orderDetailes.Price;
            p.Quintity = orderDetailes.Quintity;
            p.Tax = orderDetailes.Tax;
            p.Total = orderDetailes.Total;
            p.Amount = orderDetailes.Amount;
            p.discount = orderDetailes.discount;
            p.Order_D_key = orderDetailes.PureR_Det_Key;
            p.Product_Name = p_name.Product_Name;
            p.Order_Id = orderDetailes.PureR_ID;
            return Json(p, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveUpdateProduct(int Order_Det_Key, ProductViewModel orderDetailes)
        {
            // Quintity: Quintity, Price: Price, Amount: Amount, discount: discount, Tax: Tax, Total: Total
            PurchasesReturnDetailes order = db.PurchasesReturnDetailes.Find(Order_Det_Key);
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

        // GET: PurchasesReturns
        public ActionResult Index()
        {
            ViewBag.product = new SelectList(db.Products, "Product_ID", "Product_Name");
            ViewBag.Supplier_ID = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            ViewBag.Branch = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.PayMode = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            if (db.PurchasesReturn.Count() > 0)
            {
                var x = db.PurchasesReturn.Max(o => o.PureR_ID);

                var order_Detailes = db.PurchasesReturn.Where(o => o.PureR_ID == x).Include(o => o.PurchasesReturnDetailes).Include(o => o.Supplier).ToList();

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

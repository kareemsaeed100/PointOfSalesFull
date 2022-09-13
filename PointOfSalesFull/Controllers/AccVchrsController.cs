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
    public class AccVchrsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ExportReportMax()
        {
          
                var x = db.AccVchr.Max(o => o.vch_num);
                ReportDocument rd = new AccVchr();
                rd.Load(Path.Combine(Server.MapPath("~/Report"), "AccVchr.rpt"));
            //rd.Database.Tables[0].SetDataSource(ODM);
            rd.Database.Tables[0].SetDataSource(db.AccVchr.Where(v=>v.vch_num==x).ToList());
            rd.Database.Tables[1].SetDataSource(db.Branchies.ToList());
            rd.Database.Tables[2].SetDataSource(db.Supplier.ToList());
            rd.Database.Tables[3].SetDataSource(db.PayModes.ToList());
            Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                try
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "AccVchrList.pdf");
                }
                catch (Exception)
                {

                    throw;
                }


        }
        public ActionResult ExportReportSearch(int Vch_num)
        {


            ReportDocument rd = new AccVchr();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "AccVchr.rpt"));
            rd.Database.Tables[0].SetDataSource(db.AccVchr.Where(v => v.vch_num == Vch_num).ToList());
            rd.Database.Tables[1].SetDataSource(db.Branchies.ToList());
            rd.Database.Tables[2].SetDataSource(db.Supplier.ToList());
            rd.Database.Tables[3].SetDataSource(db.PayModes.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "AccVchrList.pdf");
            }
            catch (Exception)
            {

                throw;
            }


        }
        public JsonResult GetAccVchr()
        {
            if (db.AccVchr.Count() > 0)
            {
                var x = db.AccVchr.Max(o => o.vch_num);
                List<VchrViewModel> ODM = db.AccVchr.Where(o => o.vch_num == x).Select(o => new VchrViewModel
                {
                    vch_num = o.vch_num,
                    vch_Brn = o.vch_Brn,
                    vch_BrnName = (from b in db.Branchies
                                   where b.Branch_ID == o.vch_Brn
                                   select b.Branch_Name).FirstOrDefault(),
                    vch_DatG =o.vch_DatG.ToString(),
                    vch_paymod1 =o.vch_paymod1,
                    vch_paymod2= o.vch_paymod2,
                    vch_paymod1Name = (from p in db.PayModes
                                   where p.Pay_ID == o.vch_paymod1
                                   select p.Pay_Name).FirstOrDefault(),
                    vch_paymod2Name = (from p in db.PayModes
                                       where p.Pay_ID == o.vch_paymod2
                                       select p.Pay_Name).FirstOrDefault(),
                    Vch_PurNum = o.Vch_PurNum,
                    Vch_SubNum=o.Vch_SubNum,
                    vch_subName = (from s in db.Supplier
                                   where s.Sub_ID == o.Vch_SubNum
                                   select s.Sub_Name).FirstOrDefault(),
                    vch_cardnum=o.vch_cardnum,
                    vch_Paid1=o.vch_Paid1,
                    vch_Paid2=o.vch_Paid2,
                    vch_Wanted=o.vch_Wanted,
                    vch_Remind=o.vch_Remind
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
        public JsonResult GetAccVchrSearch(int Vch_num)
        {
            if (db.AccVchr.Count() > 0)
            {
                var x = Vch_num;
                List<VchrViewModel> ODM = db.AccVchr.Where(o => o.vch_num == x).Select(o => new VchrViewModel
                {
                    vch_num = o.vch_num,
                    vch_Brn = o.vch_Brn,
                    vch_BrnName = (from b in db.Branchies
                                   where b.Branch_ID == o.vch_Brn
                                   select b.Branch_Name).FirstOrDefault(),
                    vch_DatG = o.vch_DatG.ToString(),
                    vch_paymod1 = o.vch_paymod1,
                    vch_paymod2 = o.vch_paymod2,
                    vch_paymod1Name = (from p in db.PayModes
                                       where p.Pay_ID == o.vch_paymod1
                                       select p.Pay_Name).FirstOrDefault(),
                    vch_paymod2Name = (from p in db.PayModes
                                       where p.Pay_ID == o.vch_paymod2
                                       select p.Pay_Name).FirstOrDefault(),
                    Vch_PurNum = o.Vch_PurNum,
                    Vch_SubNum = o.Vch_SubNum,
                    vch_subName = (from s in db.Supplier
                                   where s.Sub_ID == o.Vch_SubNum
                                   select s.Sub_Name).FirstOrDefault(),
                    vch_cardnum = o.vch_cardnum,
                    vch_Paid1 = o.vch_Paid1,
                    vch_Paid2 = o.vch_Paid2,
                    vch_Wanted = o.vch_Wanted,
                    vch_Remind = o.vch_Remind
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
        [HttpPost]
        public ActionResult DeleteAccVchr(int vch_num)
        {
            //delete invoice accounting
            //var vchnum = (from v in db.AccVchr
            //              where v.Vch_PurNum == id
            //              select v.vch_num).FirstOrDefault();
            var thd_num = db.AccTransHead.FirstOrDefault(b => b.thd_MovNum == vch_num).thd_num;
            var thd_key = db.AccTransHead.FirstOrDefault(b => b.thd_MovNum == vch_num).thd_Key;
            AccTransHed hed = db.AccTransHead.Find(thd_key);
            db.AccTransHead.Remove(hed);
            var det = db.AccTransDet.RemoveRange(db.AccTransDet.Where(c => c.thd_num == thd_key));

            ///////////////////////////////////////////////////////////////////
            //delete invoice
            AccVchrs Vchr = db.AccVchr.Find(vch_num);
            db.AccVchr.Remove(Vchr);
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
        public ActionResult SaveAccVchr(int vch_Brn, DateTime vch_DatG, int vch_paymod1, int vch_paymod2, int Vch_PurNum, int Vch_SubNum, string vch_cardnum,float vch_Paid1,float vch_Paid2,float vch_Wanted,float vch_Remind)
        {
            string result = "خطأ فى انشاء الفاتورة!";
            ViewBag.vch_paymod1 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_paymod2 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_Brn = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.Vch_SubNum = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            AccVchrs accVch = new AccVchrs();
            accVch.vch_Brn = vch_Brn;
            accVch.vch_DatG = vch_DatG;
            accVch.vch_paymod1 = vch_paymod1;
            accVch.vch_paymod2 = vch_paymod2;
            accVch.Vch_PurNum = Vch_PurNum;
            accVch.Vch_SubNum = Vch_SubNum;
            accVch.vch_cardnum = vch_cardnum;
            accVch.vch_Paid1 = vch_Paid1;
            accVch.vch_Paid2 = vch_Paid2;
            accVch.vch_Wanted = vch_Wanted;
            accVch.vch_Remind = vch_Remind;
            db.AccVchr.Add(accVch);
            db.SaveChanges();
            //القيود المحاسبية
            AccTransHed hed = new AccTransHed();
            AccTransDet det1 = new AccTransDet();
            AccTransDet det2 = new AccTransDet();
            AccTransDet det3 = new AccTransDet();
            int lin = Convert.ToInt32(Convert.ToInt32(db.AccTransDet.Max(x => x.tdt_lne)) + 1);
            hed.thd_num = accVch.Vch_PurNum;
            hed.thd_MovNum = accVch.vch_num;
            hed.thd_typ = db.Defination.FirstOrDefault().PayMentAcc;
            hed.thd_brn = accVch.vch_Brn;
            hed.thd_dat = accVch.vch_DatG;
            hed.thd_des = "تسديدات المشتريات";
            db.AccTransHead.Add(hed);
            db.SaveChanges();
            ////////////////////////////////////////////
            //////////////////////////////////
            ////////////////////////////////////////////
            //1-من حساب المورد 
            det1.thd_num = hed.thd_Key;
            det1.tdt_num = accVch.vch_num;
            det1.tdt_lne = Convert.ToString(lin++);
            det1.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
            det1.tdt_brn = accVch.vch_Brn;
            det1.tdt_L1 = db.Defination.FirstOrDefault().AccSublier;
            det1.tdt_L2 = "0";
            det1.tdt_C1 = accVch.Vch_SubNum.ToString();
            det1.tdt_dr = accVch.vch_Paid1 + accVch.vch_Paid2;
            det1.tdt_cr = 0;
            det1.tdt_dat = accVch.vch_DatG;
            det1.tdt_des = "تسديدات المشتريات";
            db.AccTransDet.Add(det1);
            db.SaveChanges();
            ////////////////////////////////////////////
            ////////////////////////////////////////////
            //2-الى حساب النقدية او البنك 

            if (accVch.vch_Paid1 > 0)
            {
                det2.thd_num = hed.thd_Key;
                det2.tdt_num = accVch.vch_num;
                det2.tdt_lne = Convert.ToString(lin++);
                det2.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
                det2.tdt_brn = accVch.vch_Brn;
                if (accVch.vch_paymod1 == 1)
                {
                    //حساب النقدية
                    //det2.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                    det2.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == vch_paymod1).Select(p => p.Pay_Acc).FirstOrDefault();
                }
                else if (accVch.vch_paymod1 == 2)
                    //حساب البنك
                    //det2.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det2.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == vch_paymod1).Select(p => p.Pay_Acc).FirstOrDefault();
                det2.tdt_L2 = "0";
                det2.tdt_C1 = "0";
                det2.tdt_dr = 0;
                det2.tdt_cr = accVch.vch_Paid1;
                det2.tdt_dat = accVch.vch_DatG;
                det2.tdt_des = "تسديدات المشتريات";
                db.AccTransDet.Add(det2);
                db.SaveChanges();
            }
            if (accVch.vch_Paid2 > 0)
            {
                det3.thd_num = hed.thd_Key;
                det3.tdt_num = accVch.vch_num;
                det3.tdt_lne = Convert.ToString(lin++);
                det3.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
                det3.tdt_brn = accVch.vch_Brn;
                if (accVch.vch_paymod2 == 1)
                {
                    //حساب النقدية
                   // det3.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                    det3.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == vch_paymod2).Select(p => p.Pay_Acc).FirstOrDefault();
                }
                else if (accVch.vch_paymod2 == 2)
                    //حساب البنك
                    //det3.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det3.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == vch_paymod2).Select(p => p.Pay_Acc).FirstOrDefault();

                det3.tdt_L2 = "0";
                det3.tdt_C1 = "0";
                det3.tdt_dr = 0;
                det3.tdt_cr = accVch.vch_Paid2;
                det3.tdt_dat = accVch.vch_DatG;
                det3.tdt_des = "تسديدات المشتريات";
                db.AccTransDet.Add(det3);
                db.SaveChanges();
                result = "تم اضافة السند بنجاح!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: AccVchrs
        public ActionResult Index()
        {
            ViewBag.vch_paymod1 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_paymod2 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_Brn = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.Vch_SubNum = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            return View(db.AccVchr.ToList());
        }
        public ActionResult Index2()
        {
            ViewBag.vch_paymod1 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_paymod2 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_Brn = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.Vch_SubNum = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            return View(db.AccVchr.ToList());
        }

        // GET: AccVchrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccVchrs accVchr = db.AccVchr.Find(id);
            if (accVchr == null)
            {
                return HttpNotFound();
            }
            return View(accVchr);
        }

        // GET: AccVchrs/Create
        public ActionResult Create()
        {
            ViewBag.vch_paymod1 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_paymod2 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_Brn = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.Vch_SubNum = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            return View();
        }

        // POST: AccVchrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "vch_num,vch_Brn,vch_DatG,Vch_PurNum,Vch_SubNum,vch_Wanted")] AccVchr accVchr)
        public ActionResult Create([Bind(Include = "vch_num,vch_Brn,vch_DatG,vch_paymod1,vch_paymod2,Vch_PurNum,Vch_SubNum,vch_cardnum,vch_Paid1,vch_Paid2,vch_Wanted,vch_Remind")] AccVchrs accVchr)
        {
          
            ViewBag.vch_paymod1 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_paymod2 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name"); 
            ViewBag.vch_Brn = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.Vch_SubNum = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
 
                db.AccVchr.Add(accVchr);
                db.SaveChanges();
                //القيود المحاسبية
                AccTransHed hed = new AccTransHed();
                AccTransDet det1 = new AccTransDet();
                AccTransDet det2 = new AccTransDet();
                AccTransDet det3 = new AccTransDet();
                int lin = Convert.ToInt32(Convert.ToInt32(db.AccTransDet.Max(x => x.tdt_lne)) + 1);
                hed.thd_num =accVchr.Vch_PurNum;
                hed.thd_MovNum = accVchr.vch_num;
                hed.thd_typ = db.Defination.FirstOrDefault().PayMentAcc;
                hed.thd_brn = accVchr.vch_Brn; 
                hed.thd_dat = accVchr.vch_DatG; 
                hed.thd_des = "تسديدات المشتريات";
                db.AccTransHead.Add(hed);
                db.SaveChanges();
                ////////////////////////////////////////////
                //////////////////////////////////
                ////////////////////////////////////////////
                //1-من حساب المورد 
                    det1.thd_num = hed.thd_Key;
                    det1.tdt_num = accVchr.vch_num;
                    det1.tdt_lne = Convert.ToString(lin++);
                    det1.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
                    det1.tdt_brn = accVchr.vch_Brn;
                    det1.tdt_L1 = db.Defination.FirstOrDefault().AccSublier;
                    det1.tdt_L2 = "0";
                    det1.tdt_C1 = accVchr.Vch_SubNum.ToString();
                    det1.tdt_dr = accVchr.vch_Paid1+accVchr.vch_Paid2;
                    det1.tdt_cr = 0;
                    det1.tdt_dat = accVchr.vch_DatG;
                    det1.tdt_des = "تسديدات المشتريات";
                    db.AccTransDet.Add(det1);
                    db.SaveChanges();
                ////////////////////////////////////////////
                ////////////////////////////////////////////
                //2-الى حساب النقدية او البنك 
                 
                if (accVchr.vch_Paid1 > 0)
                {
                    det2.thd_num = hed.thd_Key;
                    det2.tdt_num = accVchr.vch_num;
                det2.tdt_lne = Convert.ToString(lin++);
                    det2.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
                    det2.tdt_brn = accVchr.vch_Brn;
                    if (accVchr.vch_paymod1 == 1)
                    {
                        //حساب النقدية
                        //det2.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                        det2.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == accVchr.vch_paymod1).Select(p => p.Pay_Acc).FirstOrDefault();

                }
                else if (accVchr.vch_paymod1 == 2)
                    //حساب البنك
                    // det2.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det2.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == accVchr.vch_paymod1).Select(p => p.Pay_Acc).FirstOrDefault();
                det2.tdt_L2 = "0";
                    det2.tdt_C1 = "0";
                    det2.tdt_dr = 0;
                    det2.tdt_cr = accVchr.vch_Paid1;
                    det2.tdt_dat = accVchr.vch_DatG;
                    det2.tdt_des = "تسديدات المشتريات";
                    db.AccTransDet.Add(det2);
                    db.SaveChanges();
                }
                if (accVchr.vch_Paid2> 0)
                {
                    det3.thd_num = hed.thd_Key;
                    det3.tdt_num = accVchr.vch_num;
                det3.tdt_lne = Convert.ToString(lin++);
                    det3.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
                    det3.tdt_brn = accVchr.vch_Brn;
                    if (accVchr.vch_paymod2 == 1)
                    {
                        //حساب النقدية
                      //  det3.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                        det3.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == accVchr.vch_paymod2).Select(p => p.Pay_Acc).FirstOrDefault();


                }
                else if (accVchr.vch_paymod2 == 2)
                    //حساب البنك
                    // det3.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det3.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == accVchr.vch_paymod2).Select(p => p.Pay_Acc).FirstOrDefault();

                det3.tdt_L2 = "0";
                    det3.tdt_C1 = "0";
                    det3.tdt_dr = 0;
                    det3.tdt_cr = accVchr.vch_Paid2;
                    det3.tdt_dat = accVchr.vch_DatG;
                    det3.tdt_des = "تسديدات المشتريات";
                    db.AccTransDet.Add(det3);
                    db.SaveChanges();
                }
                ////////////////////////////////////////////
                return RedirectToAction("Index");
        }

        // GET: AccVchrs/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.vch_paymod1 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_paymod2 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_Brn = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.Vch_SubNum = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccVchrs accVchr = db.AccVchr.Find(id);
            if (accVchr == null)
            {
                return HttpNotFound();
            }
            return View(accVchr);
        }

        // POST: AccVchrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vch_num,vch_Brn,vch_DatG,vch_typ,vch_paymod,Vch_SubNum,Vch_PurNum,vch_cardnum,vch_Paid,vch_Wanted,vch_Remind")] AccVchrs accVchr)
        {
            ViewBag.vch_paymod1 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_paymod2 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.vch_Brn = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
            ViewBag.Vch_SubNum = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
            ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
            if (ModelState.IsValid)
            {
                db.Entry(accVchr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accVchr);
        }

        // GET: AccVchrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccVchrs accVchr = db.AccVchr.Find(id);
            if (accVchr == null)
            {
                return HttpNotFound();
            }
            return View(accVchr);
        }

        // POST: AccVchrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccVchrs accVchr = db.AccVchr.Find(id);
            db.AccVchr.Remove(accVchr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult GetPurelist(int Sub_ID)
        {
            if (db.PurchasesInvoices.Count() > 0)
            {
                List<ProductViewModel> Purelist = db.PurchasesInvoices.Where(o => o.Sub_ID == Sub_ID).Select(o => new ProductViewModel
                {
                    Order_Id = o.Pure_ID,
                }).ToList();
                return Json(Purelist, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetPureValue(int Pur_Id)
        {
            if (db.PurchasesInvoices.Count() > 0)
            {
                var vchnum = (from v in db.AccVchr
                              where v.Vch_PurNum == Pur_Id
                              select v.vch_num).FirstOrDefault();
                List<ProductViewModel> Purelist = db.AccTransDet.Where(o => o.AccTransHed.thd_num == Pur_Id && o.tdt_L1 == (db.Defination.FirstOrDefault().AccSublier) && o.tdt_C1 !="0").Select(o => new ProductViewModel
                {
                    cr = o.tdt_cr,
                    dr = o.tdt_dr,
                }).ToList();
                return Json(Purelist, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetDebtValue(int order_num)
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

                ViewBag.vch_paymod1 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                ViewBag.vch_paymod2 = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
                ViewBag.vch_Brn = new SelectList(db.Branchies, "Branch_ID", "Branch_Name");
                ViewBag.Vch_SubNum = new SelectList(db.Supplier, "Sub_ID", "Sub_Name");
                ViewBag.Saller_ID = new SelectList(db.Salleres, "Saller_ID", "Saler_Name");
                return Json(detaileslist, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAccVchrByID(int Vch_num)
        {
            //System.Threading.Thread.Sleep(4000);
            AccVchrs accvchr = db.AccVchr.Find(Vch_num);
            VchrViewModel v = new VchrViewModel();
            v.vch_paymod1 = accvchr.vch_paymod1;
            v.vch_paymod2 = accvchr.vch_paymod2;
            v.vch_paymod1Name = (from p in db.PayModes
                                 where p.Pay_ID == v.vch_paymod1
                                 select p.Pay_Name).FirstOrDefault();
            v.vch_paymod2Name = (from p in db.PayModes
                                 where p.Pay_ID == v.vch_paymod2
                                 select p.Pay_Name).FirstOrDefault();
            v.vch_Paid1 = accvchr.vch_Paid1;
            v.vch_Paid2 = accvchr.vch_Paid2;
            v.vch_subName = (from s in db.Supplier
                             where s.Sub_ID == v.Vch_SubNum
                             select s.Sub_Name).FirstOrDefault();
            v.Vch_PurNum = accvchr.Vch_PurNum;
            v.vch_BrnName = (from b in db.Branchies
                             where b.Branch_ID == accvchr.vch_Brn
                             select b.Branch_Name).FirstOrDefault();
            v.vch_num = accvchr.vch_num;
            v.vch_cardnum = accvchr.vch_cardnum;
            v.vch_DatG =accvchr.vch_DatG.ToString() ;
            v.vch_Wanted = accvchr.vch_Wanted;
            return Json(v, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveUpdateAccVchr(int vch_Num, DateTime vch_DatG, int vch_paymod1, int vch_paymod2,string vch_cardnum, float vch_Paid1,float vch_Paid2 )
        {
            int val=0;
            AccVchrs accvchr = db.AccVchr.Find(vch_Num);
            accvchr.vch_DatG = Convert.ToDateTime(vch_DatG);
            accvchr.vch_paymod1 = vch_paymod1;
            accvchr.vch_paymod2 = vch_paymod2;
            accvchr.vch_Paid1 = vch_Paid1;
            accvchr.vch_Paid2 = vch_Paid2;
            accvchr.vch_cardnum = vch_cardnum;
            accvchr.vch_Remind = accvchr.vch_Wanted - (accvchr.vch_Paid1 + accvchr.vch_Paid2);
            db.Entry(accvchr).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //القيود المحاسبية

            AccTransDet det1 = new AccTransDet();
            AccTransDet det2 = new AccTransDet();
            AccTransDet det3 = new AccTransDet();
            int lin = Convert.ToInt32(Convert.ToInt32(db.AccTransDet.Max(x => x.tdt_lne)) + 1);
            ////////////////////////////////////////////
            //1-من حساب المورد 
            var thd_key = db.AccTransHead.FirstOrDefault(b => b.thd_MovNum == accvchr.vch_num).thd_Key;
            var det = db.AccTransDet.RemoveRange(db.AccTransDet.Where(c => c.thd_num == thd_key));
            db.SaveChanges();
            det1.thd_num = thd_key;
            det1.tdt_num = accvchr.vch_num;
            det1.tdt_lne = Convert.ToString(lin++);
            det1.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
            det1.tdt_brn = accvchr.vch_Brn;
            det1.tdt_L1 = db.Defination.FirstOrDefault().AccSublier;
            det1.tdt_L2 = "0";
            det1.tdt_C1 = accvchr.Vch_SubNum.ToString();
            det1.tdt_dr = accvchr.vch_Paid1 + accvchr.vch_Paid2;
            det1.tdt_cr = 0;
            det1.tdt_dat = accvchr.vch_DatG;
            det1.tdt_des = "تسديدات المشتريات";
            db.AccTransDet.Add(det1);
            db.SaveChanges();
            ////////////////////////////////////////////
            ////////////////////////////////////////////
            //2-الى حساب النقدية او البنك 

            if (accvchr.vch_Paid1 > 0)
            {
                det2.thd_num = thd_key;
                det2.tdt_num = accvchr.vch_num;
                det2.tdt_lne = Convert.ToString(lin++);
                det2.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
                det2.tdt_brn = accvchr.vch_Brn;
                if (accvchr.vch_paymod1 == 1)
                {
                    //حساب النقدية
                   // det2.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                    det2.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == accvchr.vch_paymod1).Select(p => p.Pay_Acc).FirstOrDefault();

                }
                else if (accvchr.vch_paymod1 == 2)
                    //حساب البنك
                    //det2.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det2.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == accvchr.vch_paymod1).Select(p => p.Pay_Acc).FirstOrDefault();

                det2.tdt_L2 = "0";
                det2.tdt_C1 = "0";
                det2.tdt_dr = 0;
                det2.tdt_cr = accvchr.vch_Paid1;
                det2.tdt_dat = accvchr.vch_DatG;
                det2.tdt_des = "تسديدات المشتريات";
                db.AccTransDet.Add(det2);
                db.SaveChanges();
            }
            if (accvchr.vch_Paid2 > 0)
            {
                det3.thd_num = thd_key;
                det3.tdt_num = accvchr.vch_num;
                det3.tdt_lne = Convert.ToString(lin++);
                det3.tdt_typ = db.Defination.FirstOrDefault().PayMentAcc;
                det3.tdt_brn = accvchr.vch_Brn;
                if (accvchr.vch_paymod2 == 1)
                {
                    //حساب النقدية
                    //det3.tdt_L1 = db.Defination.FirstOrDefault().AccCash;
                    det3.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == accvchr.vch_paymod2).Select(p => p.Pay_Acc).FirstOrDefault();

                }
                else if (accvchr.vch_paymod2 == 2)
                    //حساب البنك
                  //  det3.tdt_L1 = db.Defination.FirstOrDefault().AccBank;
                    det3.tdt_L1 = db.PayModes.Where(p => p.Pay_ID == accvchr.vch_paymod2).Select(p => p.Pay_Acc).FirstOrDefault();
 
                det3.tdt_L2 = "0";
                det3.tdt_C1 = "0";
                det3.tdt_dr = 0;
                det3.tdt_cr = accvchr.vch_Paid2;
                det3.tdt_dat = accvchr.vch_DatG;
                det3.tdt_des = "تسديدات المشتريات";
                db.AccTransDet.Add(det3);
                val =  db.SaveChanges();
            }
            if (val > 0)
                return Json(new { StatUs = 1, Message = " تمت عملية التعديل بنجاح" },
                    JsonRequestBehavior.AllowGet);
            else
                return Json(new { StatUs = 0, Message = " لم تتم عملية التعديل بنجاح" },
                JsonRequestBehavior.AllowGet);

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

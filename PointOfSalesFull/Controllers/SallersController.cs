using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointOfSalesFull.Models;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using PointOfSalesFull.Report;
using CrystalDecisions.Shared;

namespace PointOfSalesFull.Controllers
{
    public class SallersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ExportReport(string ReportType)
        {
            ReportDocument rd = new Salleres();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Customeres.rpt"));
            rd.Database.Tables[0].SetDataSource(db.Salleres.ToList());
            rd.Database.Tables[1].SetDataSource(db.Nationalities.ToList());

            //crystalReportViewer1.ReportSource = report;
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ExportFormatType formatetype = ExportFormatType.NoFormat;

            switch (ReportType)
            {
                case "Word":
                    formatetype = ExportFormatType.WordForWindows;
                    break;
                case "Pdf":
                    formatetype = ExportFormatType.PortableDocFormat;
                    break;
                case "Excel":
                    formatetype = ExportFormatType.Excel;
                    break;
                case "Csv":
                    formatetype = ExportFormatType.CharacterSeparatedValues;
                    break;
            }
            try
            {
                Stream stream = rd.ExportToStream(formatetype);
                stream.Seek(0, SeekOrigin.Begin);
                if (ReportType == "Word")
                    return File(stream, "application/vnd.ms-word", "SallerList.doc");

                else if (ReportType == "Csv")
                    return File(stream, "application/vnd.ms-excel", "SallerList.csv");
                else if (ReportType == "Excel")
                    return File(stream, "application/vnd.ms-excel", "SallerList.xls");
                else
                    return File(stream, "application/pdf", "SallerList.pdf");
              

            }
            catch (Exception)
            {

                throw;
            }

        }
        // GET: Sallers
        public ActionResult Index()
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");

            return View(db.Salleres.ToList());
        }

        // GET: Sallers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saller saller = db.Salleres.Find(id);
            if (saller == null)
            {
                return HttpNotFound();
            }
            return View(saller);
        }

        // GET: Sallers/Create
        public ActionResult Create()
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            return View();
        }

        // POST: Sallers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Saller_ID,Saler_Name,Saler_EName,Saler_Email,Saler_Phon,Saler_Adress,Nat_ID")] Saller saller)
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (ModelState.IsValid)
            {
                db.Salleres.Add(saller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saller);
        }

        // GET: Sallers/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saller saller = db.Salleres.Find(id);
            if (saller == null)
            {
                return HttpNotFound();
            }
            return View(saller);
        }

        // POST: Sallers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Saller_ID,Saler_Name,Saler_EName,Saler_Email,Saler_Phon,Saler_Adress,Nat_ID")] Saller saller)
        {
          
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (ModelState.IsValid)
            {
                db.Entry(saller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saller);
        }

        // GET: Sallers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saller saller = db.Salleres.Find(id);
            if (saller == null)
            {
                return HttpNotFound();
            }
            return View(saller);
        }

        // POST: Sallers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Saller saller = db.Salleres.Find(id);
            db.Salleres.Remove(saller);
            db.SaveChanges();
            return RedirectToAction("Index");
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

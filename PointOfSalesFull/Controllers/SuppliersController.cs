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
using CrystalDecisions.Shared;

namespace PointOfSalesFull.Controllers
{
    public class SuppliersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ExportReport(string ReportType)
        {
            ReportDocument rd = new Supplieres();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Supplieres.rpt"));
            rd.Database.Tables[0].SetDataSource(db.Supplier.ToList());
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
                    return File(stream, "application/vnd.ms-word", "SupplierList.doc");

                else if (ReportType == "Csv")
                    return File(stream, "application/vnd.ms-excel", "SupplierList.csv");
                else if (ReportType == "Excel")
                    return File(stream, "application/vnd.ms-excel", "SupplierList.xls");
                else
                    return File(stream, "application/pdf", "SupplierList.pdf");
         
            }
            catch (Exception)
            {

                throw;
            }

        }
        // GET: Suppliers
        public ActionResult Index()
        {
           
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            return View(db.Supplier.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Supplier.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sub_ID,Sub_Name,Sub_EName,Sub_Email,Sub_Phon,Sub_Adress,Nat_ID")] Supplier supplier)
        {
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (ModelState.IsValid)
            {
                db.Supplier.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Supplier.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sub_ID,Sub_Name,Sub_EName,Sub_Email,Sub_Phon,Sub_Adress,Nat_ID")] Supplier supplier)
        {
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Supplier.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Supplier.Find(id);
            db.Supplier.Remove(supplier);
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

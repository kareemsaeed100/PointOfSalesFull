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
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ExportReport(string ReportType)
        {
            ReportDocument rd = new Customeres();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Customeres.rpt"));
            rd.Database.Tables[0].SetDataSource(db.Customers.ToList());
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
                    return File(stream, "application/vnd.ms-word", "CustomesList.doc");

                else if (ReportType == "Csv")
                    return File(stream, "application/vnd.ms-excel", "CustomesList.csv");
                else if (ReportType == "Excel")
                    return File(stream, "application/vnd.ms-excel", "CustomesList.xls");
                else
                    return File(stream, "application/pdf", "CustomesList.pdf");
    
            }
            catch (Exception)
            {

                throw;
            }

        }
        // GET: Customers
        public ActionResult Index()
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customes_ID,First_Name,Nat_ID,Last_Name,First_EName,Last_EName,Phon,Email")] Customer customer)
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customes_ID,First_Name,Nat_ID,Last_Name,First_EName,Last_EName,Phon,Email")] Customer customer)
        {
            ViewBag.vch_paymod = new SelectList(db.PayModes, "Pay_ID", "Pay_Name");
            ViewBag.Nationality = new SelectList(db.Nationalities, "Nat_ID", "Nat_Name");
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

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
using CrystalDecisions.Shared;

namespace PointOfSalesFull.Controllers
{
    public class PayModesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ExportReport(string ReportType)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "PayModes.rpt"));
            rd.SetDataSource(db.PayModes.ToList());
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
                    return File(stream, "application/vnd.ms-word", "PayModeList.doc");

                else if (ReportType == "Csv")
                    return File(stream, "application/vnd.ms-excel", "PayModeList.csv");
                else if (ReportType == "Excel")
                    return File(stream, "application/vnd.ms-excel", "PayModeList.xls");
                else
                    return File(stream, "application/pdf", "PayModeList.pdf");
           
            }
            catch (Exception)
            {

                throw;
            }

        }
        // GET: PayModes
        public ActionResult Index()
        {
            return View(db.PayModes.ToList());
        }

        // GET: PayModes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayMode payMode = db.PayModes.Find(id);
            if (payMode == null)
            {
                return HttpNotFound();
            }
            return View(payMode);
        }

        // GET: PayModes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayModes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pay_ID,Pay_Name,Pay_EName,Pay_Acc")] PayMode payMode)
        {
            if (ModelState.IsValid)
            {
                db.PayModes.Add(payMode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payMode);
        }

        // GET: PayModes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayMode payMode = db.PayModes.Find(id);
            if (payMode == null)
            {
                return HttpNotFound();
            }
            return View(payMode);
        }

        // POST: PayModes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pay_ID,Pay_Name,Pay_EName,Pay_Acc")] PayMode payMode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payMode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payMode);
        }

        // GET: PayModes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayMode payMode = db.PayModes.Find(id);
            if (payMode == null)
            {
                return HttpNotFound();
            }
            return View(payMode);
        }

        // POST: PayModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayMode payMode = db.PayModes.Find(id);
            db.PayModes.Remove(payMode);
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

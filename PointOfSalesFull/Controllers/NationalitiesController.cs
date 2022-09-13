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
using CrystalDecisions.Shared;

namespace PointOfSalesFull.Controllers
{
    public class NationalitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ExportReport(string ReportType)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Nationality.rpt"));
            rd.SetDataSource(db.Nationalities.ToList());
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
                    return File(stream, "application/vnd.ms-word", "NationalityList.doc");

                else if (ReportType == "Csv")
                    return File(stream, "application/vnd.ms-excel", "NationalityList.csv");
                else if (ReportType == "Excel")
                    return File(stream, "application/vnd.ms-excel", "NationalityList.xls");
                else
                    return File(stream, "application/pdf", "NationalityList.pdf");
            
            }
            catch (Exception)
            {

                throw;
            }

        }
        // GET: Nationalities
        public ActionResult Index()
        {
            return View(db.Nationalities.ToList());
        }

        // GET: Nationalities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nationality nationality = db.Nationalities.Find(id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        // GET: Nationalities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nationalities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nat_ID,Nat_Name,Nat_EName")] Nationality nationality)
        {
            if (ModelState.IsValid)
            {
                db.Nationalities.Add(nationality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nationality);
        }

        // GET: Nationalities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nationality nationality = db.Nationalities.Find(id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        // POST: Nationalities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nat_ID,Nat_Name,Nat_EName")] Nationality nationality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nationality);
        }

        // GET: Nationalities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nationality nationality = db.Nationalities.Find(id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        // POST: Nationalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nationality nationality = db.Nationalities.Find(id);
            db.Nationalities.Remove(nationality);
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

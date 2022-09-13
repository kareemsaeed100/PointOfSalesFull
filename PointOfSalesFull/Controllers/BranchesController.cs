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
    public class BranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ExportReport(string ReportType)
        {
          
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Branchies.rpt"));
            rd.SetDataSource(db.Branchies.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            ExportFormatType formatetype = ExportFormatType.NoFormat;
       
            switch(ReportType)
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
            Stream stream = rd.ExportToStream(formatetype);
                stream.Seek(0, SeekOrigin.Begin);
                if(ReportType== "Word")
                    return File(stream, "application/msword", "BranchiesList.doc");
               
                else if (ReportType == "Csv")
                    return File(stream, "application/vnd.ms-excel", "BranchiesList.csv");
                else if (ReportType == "Excel")
                    return File(stream, "application/vnd.ms-excel", "BranchiesList.xls");
                else 
                return File(stream, "application/pdf", "BranchiesList.pdf");
        }
        // GET: Branches
        public ActionResult Index()
        {
            return View(db.Branchies.ToList());
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branchies.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Branch_ID,Branch_Name,Branch_EName")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                db.Branchies.Add(branches);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branches);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branchies.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Branch_ID,Branch_Name,Branch_EName")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branches).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branches);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branches branches = db.Branchies.Find(id);
            if (branches == null)
            {
                return HttpNotFound();
            }
            return View(branches);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branches branches = db.Branchies.Find(id);
            db.Branchies.Remove(branches);
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

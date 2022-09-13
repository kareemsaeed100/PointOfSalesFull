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
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ExportReport(string ReportType)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Category.rpt"));
            rd.SetDataSource(db.Categories.ToList());
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
                    return File(stream, "application/vnd.ms-word", "CategoriesList.doc");

                else if (ReportType == "Csv")
                    return File(stream, "application/vnd.ms-excel", "CategoriesList.csv");
                else if (ReportType == "Excel")
                    return File(stream, "application/vnd.ms-excel", "CategoriesList.xls");
                else
                    return File(stream, "application/pdf", "CategoriesList.pdf");
       
            }
            catch (Exception)
            {

                throw;
            }

        }
        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.department);
            return View(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.Dep_ID = new SelectList(db.Departments, "Deb_ID", "Deb_Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cat_ID,Cat_Descrption,Cat_EDescrption,Dep_ID")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Dep_ID = new SelectList(db.Departments, "Deb_ID", "Deb_Name", category.Dep_ID);
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dep_ID = new SelectList(db.Departments, "Deb_ID", "Deb_Name", category.Dep_ID);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cat_ID,Cat_Descrption,Cat_EDescrption,Dep_ID")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Dep_ID = new SelectList(db.Departments, "Deb_ID", "Deb_Name", category.Dep_ID);
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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

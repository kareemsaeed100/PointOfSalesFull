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
using PointOfSalesFull.Report;
using System.IO;
using CrystalDecisions.Shared;

namespace PointOfSalesFull.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ExportReport(string ReportType)
        {
            ReportDocument rd = new Products();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Products.rpt"));
            var products = (from p in db.Products
                            select new {
                                p.Product_ID,
                                p.Product_Name,
                                p.Price,
                                p.Cat_ID,
                                p.Uint_Id,
                                p.Brand_Id,
                                p.Mov_State,
                                p.Tax_State,
                                p.Return
                            }).ToList();
            //rd.SetDataSource(products);
            rd.Database.Tables[0].SetDataSource(products.ToList());
            rd.Database.Tables[1].SetDataSource(db.Categories.ToList());
            rd.Database.Tables[2].SetDataSource(db.Brands.ToList());
            rd.Database.Tables[3].SetDataSource(db.Units.ToList());
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
                    return File(stream, "application/vnd.ms-word", "ProductList.doc");

                else if (ReportType == "Csv")
                    return File(stream, "application/vnd.ms-excel", "ProductList.csv");
                else if (ReportType == "Excel")
                    return File(stream, "application/vnd.ms-excel", "ProductList.xls");
                else
                    return File(stream, "application/pdf", "ProductList.pdf");

            }
            catch (Exception)
            {

                throw;
            }

        }
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Cat_ID = new SelectList(db.Categories, "Cat_ID", "Cat_Descrption");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Product_Name,Quintity,Price,Image,Cat_ID,Brand_Id,Uint_Id,For_Sale,Tax_State,Return,Mov_State")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cat_ID = new SelectList(db.Categories, "Cat_ID", "Cat_Descrption", product.Cat_ID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_ID = new SelectList(db.Categories, "Cat_ID", "Cat_Descrption", product.Cat_ID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_ID,Product_Name,Quintity,Price,Image,Cat_ID,Brand_Id,Uint_Id,For_Sale,Tax_State,Return,Mov_State")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_ID = new SelectList(db.Categories, "Cat_ID", "Cat_Descrption", product.Cat_ID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

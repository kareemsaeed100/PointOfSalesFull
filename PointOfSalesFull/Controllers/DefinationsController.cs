using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointOfSalesFull.Models;

namespace PointOfSalesFull.Controllers
{
    public class DefinationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Definations
        public ActionResult Index()
        {
            return View(db.Defination.ToList());
        }

        // GET: Definations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Defination defination = db.Defination.Find(id);
            if (defination == null)
            {
                return HttpNotFound();
            }
            return View(defination);
        }

        // GET: Definations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Definations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,AccCustomer,AccCash,AccSalesRevenues,AccTax,AccSalesCost,AccInv,AccBank,AccSublier,PureAcc,PureRAcc,SalesAcc,SalesRAcc,ReciptAcc,PayMentAcc")] Defination defination)
        {
            if (ModelState.IsValid)
            {
                db.Defination.Add(defination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(defination);
        }

        // GET: Definations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Defination defination = db.Defination.Find(id);
            if (defination == null)
            {
                return HttpNotFound();
            }
            return View(defination);
        }

        // POST: Definations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,AccCustomer,AccCash,AccSalesRevenues,AccTax,AccSalesCost,AccInv,AccBank,AccSublier,PureAcc,PureRAcc,SalesAcc,SalesRAcc,ReciptAcc,PayMentAcc")] Defination defination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(defination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(defination);
        }

        // GET: Definations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Defination defination = db.Defination.Find(id);
            if (defination == null)
            {
                return HttpNotFound();
            }
            return View(defination);
        }

        // POST: Definations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Defination defination = db.Defination.Find(id);
            db.Defination.Remove(defination);
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

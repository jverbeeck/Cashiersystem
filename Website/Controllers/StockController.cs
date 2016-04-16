using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Website.DAL;
using Website.Models;

namespace Website.Controllers
{
    public class StockController : BaseController
    {
        private CashierContext db = new CashierContext();

        // GET: StockItem
        public ActionResult Index()
        {
            return !_auth.IsAuthenticated || !_auth.IsAdmin ? View("Error") : View(db.StockItems.ToList());
        }

        // GET: StockItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // GET: StockItem/Create
        public ActionResult Create()
        {
            ViewBag.Categories = DataAccess.GetCategories();
            return View();
        }

        // POST: StockItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.StockItems.Add(stockItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockItem);
        }

        // GET: StockItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: StockItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockId,Name,Price,AvailableQuantity")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockItem);
        }

        // GET: StockItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: StockItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockItem stockItem = db.StockItems.Find(id);
            db.StockItems.Remove(stockItem);
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

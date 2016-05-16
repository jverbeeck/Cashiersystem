using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Extensions;
using DAL;
using Website.DAL;
using Website.Models;

namespace Website.Controllers
{
    public class OrdersController : BaseController
    {
        private CashierContext db = new CashierContext();

        // GET: Orders
        public ActionResult Index()
        {
            return !_auth.IsAuthenticated ? View("Error") : View();
        }

        public ActionResult List()
        {
            return !_auth.IsAuthenticated ? View("Error") : View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        // GET: Orders/Create


        public ActionResult Create()
        {
            ViewBag.stock = DataAccess.GetStockItems();
            return !_auth.IsAuthenticated ? View("Error") : View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( OrderItem[] orderItems)
        {
            if (ModelState.IsValid)
            {

                var order = BLL.OrderBLL.CreateOrder(orderItems);

                db.Orders.Add(order);
                db.SaveChanges();

                return RedirectToAction("List");
            }

            return View();
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.stock = DataAccess.GetStockItems();
            ViewBag.CurrentOrder = order;

            return View(db.Orders.ToList());
        }

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "OrderId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}

        // GET: Orders/Delete/5

        public ActionResult Confirm(int? id) //todo
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            order.HasConfirmed = true;
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



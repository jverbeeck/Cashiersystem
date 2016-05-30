using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
        public ActionResult Create(OrderItem[] orderItems)
        {
            var items = orderItems.ToList();

            if (ModelState.IsValid)
            {
                var tableNumber = string.Empty;
                var orderId = 0;

                var orderItemTableNumber = items.FirstOrDefault(w => w.StockItemId == 0);
                if (orderItemTableNumber != null)
                {
                    //Get table number and remove item from list
                    tableNumber = orderItemTableNumber.Name;
                    items.Remove(orderItemTableNumber);
                }

                var orderItemOrderId = items.FirstOrDefault(w => w.StockItemId == -1);
                if (orderItemOrderId != null)
                {
                    orderId = Int32.Parse(orderItemOrderId.Name);
                    items.Remove(orderItemOrderId);

                }


                var order = orderId == 0 ?
                 BLL.OrderBLL.CreateOrder(items, tableNumber) :
                 db.Orders.FirstOrDefault(w => w.OrderId == orderId);

                if (orderId != 0)
                {
                    //remove order
                    db.Orders.Remove(db.Orders.FirstOrDefault(w => w.OrderId == orderId));
                    //remove currentOrderItems
                    var currentOrderItems = db.OrderItems.Where(w => w.OrderId == orderId);
                    foreach (var currentOrderItem in currentOrderItems)
                    {
                        db.OrderItems.Remove(currentOrderItem);
                    }
                }

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

        public ActionResult Update(OrderItem[] orderItems)
        {
            return null;
        }


        [HttpPost]
        public ActionResult Confirm(OrderItem[] orderItems)
        {

            var items = orderItems.ToList();
            var orderId = 0;
            var tableNumber = string.Empty;
            Order order = new Order();
            var firstOrDefault = orderItems.FirstOrDefault(w => !string.IsNullOrEmpty(w.Name) && w.Name.Equals("scenario"));

            if (firstOrDefault != null)
            {
                var scenario = (Enums.Scenario)firstOrDefault.StockItemId;
                var orderItemTableNumber = items.FirstOrDefault(w => w.StockItemId == -4);
                if (orderItemTableNumber != null)
                {
                    //Get table number and remove item from list
                    tableNumber = orderItemTableNumber.Name;
                    items.Remove(orderItemTableNumber);
                }

                var orderItemOrderId = items.FirstOrDefault(w => w.StockItemId == -3 && !string.IsNullOrEmpty(w.Name));
                if (orderItemOrderId != null)
                {
                    //Get table number and remove item from list
                    orderId = int.Parse(orderItemOrderId.Name);
                    items.Remove(orderItemOrderId);
                }

                switch (scenario)
                {
                    //first add the order and return it to the confirmation view
                    case Enums.Scenario.Create:
                        order = BLL.OrderBLL.CreateOrder(items, tableNumber);
                        break;

                    //retrieve the order from the database, if changed, update and return it, else, return it
                    case Enums.Scenario.Update:
                        var currentOrderItems = db.OrderItems.Where(w => w.OrderId == orderId).ToList();
                        order = db.Orders.FirstOrDefault(w => w.OrderId == orderId);
                        db.Orders.Remove(order);

                        db.SaveChanges();


                        order = BLL.OrderBLL.CreateOrder(currentOrderItems, tableNumber);
                        db.Orders.Add(order);
                        break;
                }
            }
            db.SaveChanges();

            return View(order);
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



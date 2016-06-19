using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL;
using Website.DAL;
using Website.Models;
using BLL.Extensions;

namespace Website.Controllers
{
    /// <summary>
    /// This is a controller that handles all the order actions in the project
    /// </summary>
    public class OrdersController : BaseController
    {
        private CashierContext db = new CashierContext();

        /// <summary>
        /// Retrieve the orders index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return !_auth.IsAuthenticated ? View("Error") : View();
        }

        /// <summary>
        /// List all the orders
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var orders = db.Orders.Where(w => !w.HasConfirmed).ToList();
            return !_auth.IsAuthenticated ? View("Error") : View(orders);
        }

        /// <summary>
        /// GET gets the create order page
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(int tableNo)
        {
            ViewBag.stock = DataAccess.GetStockItems();
            return !_auth.IsAuthenticated ? View("Error") : View(tableNo);
        }

        /// <summary>
        /// POST Create an order
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(OrderItem[] orderItems)
        {
            var items = orderItems.ToList();

            if (ModelState.IsValid)
            {
                var tableNumber = string.Empty;
                var orderId = 0;

                var orderItemTableNumber = items.FirstOrDefault(w => w.StockItemId == (int)Enums.Special.TableNumber);
                if (orderItemTableNumber != null)
                {
                    //Get table number and remove item from list
                    tableNumber = orderItemTableNumber.Name;
                    items.Remove(orderItemTableNumber);
                }

                var orderItemOrderId = items.FirstOrDefault(w => w.StockItemId == (int)Enums.Special.OrderId);
                if (orderItemOrderId != null)
                {
                    orderId = int.Parse(orderItemOrderId.Name);
                    items.Remove(orderItemOrderId);
                }

                var order = new Order();

                if (orderId == 0) //case create
                {
                    order = BLL.OrderBLL.CreateOrder(items, tableNumber);
                }
                else //case update
                {
                    //get and remove order
                    order = db.Orders.FirstOrDefault(w => w.OrderId == orderId);
                    tableNumber = order.TableNumber.ToString();
                    db.Orders.Remove(order);

                    db.SaveChanges();

                    //create new order
                    order = BLL.OrderBLL.CreateOrder(items, tableNumber);
                }

                db.Orders.Add(order);
                db.SaveChanges();

                return RedirectToAction("List");
            }

            return View();
        }

        /// <summary>
        /// Checks wether the tablenumber exists in the current orders
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <returns>Returns a create order view or a edit order view depending on this existance</returns>
        public ActionResult CheckTableNumber(int tableNumber)
        {

            var orders = db.Orders.Where(w => w.TableNumber == tableNumber && !w.HasConfirmed).ToList();
            return !orders.Any() ? RedirectToAction("Create", new { tableNo = tableNumber}) : RedirectToAction("Edit", new { id = orders.FirstOrDefault().OrderId });
        }

        /// <summary>
        /// Edit an order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            return View(order);
        }

        /// <summary>
        /// Confirm an order 
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
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
                items.Remove(firstOrDefault);

                var orderItemTableNumber = items.FirstOrDefault(w => w.StockItemId == (int)Enums.Special.TableNumber);
                if (orderItemTableNumber != null)
                {
                    //Get table number and remove item from list
                    tableNumber = orderItemTableNumber.Name;
                    items.Remove(orderItemTableNumber);
                }

                var orderItemOrderId = items.FirstOrDefault(w => w.StockItemId == (int)Enums.Special.OrderId && !string.IsNullOrEmpty(w.Name));
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
                        tableNumber = order.TableNumber.ToString();
                        db.Orders.Remove(order);

                        db.SaveChanges();

                        order = BLL.OrderBLL.CreateOrder(currentOrderItems, tableNumber);
                        break;

                    case Enums.Scenario.Confirm:
                        order = db.Orders.FirstOrDefault(f => f.OrderId == orderId);
                        return View(order);
                }
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return View(order);
        }

        /// <summary>
        /// Complete the confirmation
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Complete(int orderId)
        {
            var order = db.Orders.FirstOrDefault(f => f.OrderId == orderId);
            if (order != null)
            {
                order.HasConfirmed = true;

                //update stockitem quantity
                foreach (var orderItem in order.OrderItems)
                {
                    var stockItem = db.StockItems.FirstOrDefault(f => f.StockItemId == orderItem.StockItemId);
                    if (stockItem != null) stockItem.AvailableQuantity -= orderItem.Quantity;
                }
            }
            db.SaveChanges();
            return RedirectToAction("List");
        }

        /// <summary>
        /// Remove an order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(int orderId)
        {
            var order = db.Orders.FirstOrDefault(f => f.OrderId == orderId);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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



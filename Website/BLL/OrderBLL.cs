using System.Collections.Generic;
using System.Linq;
using DAL;
using Website.Models;

namespace Website.BLL
{
    /// <summary>
    /// This class handles all the logic for the orders
    /// </summary>
    public static class OrderBLL
    {
        /// <summary>
        /// This method creates an order depending on inserted items and a tablenumber
        /// </summary>
        /// <param name="orderItems"></param>
        /// <param name="tableNumber"></param>
        /// <returns></returns>
        public static Order CreateOrder(List<OrderItem> orderItems, string tableNumber)
        {
            var order = new Order { HasConfirmed = false };

            var stockItems = DataAccess.GetStockItems();
            foreach (var orderItem in orderItems)
            {
                var stockItem = stockItems.FirstOrDefault(w => w.StockItemId == orderItem.StockItemId);
                if (stockItem == null) continue;

                orderItem.Name = stockItem.Name;
                orderItem.UnitPrice = stockItem.Price;
                orderItem.SubTotal = orderItem.Quantity * orderItem.UnitPrice;
                orderItem.OrderId = order.OrderId; //FK

                order.TotalPrice += orderItem.SubTotal;
                order.TotalQuantity += orderItem.Quantity;
            }

            order.TableNumber = string.IsNullOrEmpty(tableNumber) ? 0 : int.Parse(tableNumber);
            order.OrderItems = orderItems.ToList();

            return order;
        }

     }
}
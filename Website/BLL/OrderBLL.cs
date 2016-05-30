using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using Website.Models;

namespace Website.BLL
{
    public static class OrderBLL
    {
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
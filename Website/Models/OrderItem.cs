using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal { get; set; }
        public int OrderId { get; set; }
    }
}
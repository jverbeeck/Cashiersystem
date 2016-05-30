using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

        public bool HasConfirmed { get; set; }

        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }

        public int TableNumber { get; set; }

    }
}
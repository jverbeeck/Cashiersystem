using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class StockItem
    {
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int AvailableQuantity { get; set; }

    }
}
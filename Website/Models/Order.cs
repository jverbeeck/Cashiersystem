using System.Collections.Generic;

namespace Website.Models
{
    /// <summary>
    /// An Order 
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The Id for this order object
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// A list of orderitems 
        /// </summary>
        public virtual List<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Determines wether the order is confirmed
        /// </summary>
        public bool HasConfirmed { get; set; }

        /// <summary>
        /// How many items an order consists of
        /// </summary>
        public int TotalQuantity { get; set; }
        /// <summary>
        /// The total price of an order
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// The tablenumber of an order
        /// </summary>
        public int TableNumber { get; set; }

    }
}
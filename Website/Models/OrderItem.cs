namespace Website.Models
{
    /// <summary>
    /// An Orderitem
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// The Id for an orderitem
        /// </summary>
        public int OrderItemId { get; set; }
        /// <summary>
        /// FK to the stockitem
        /// </summary>
        public int StockItemId { get; set; }
        /// <summary>
        /// The name of the orderitem
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The quantity of the orderitem
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// How much an orderitem costs
        /// </summary>
        public double UnitPrice { get; set; }
        /// <summary>
        /// A subtotal per orderItem type
        /// </summary>
        public double SubTotal { get; set; }
        /// <summary>
        /// FK to link an orderitem with an order
        /// </summary>
        public int OrderId { get; set; }
    }
}
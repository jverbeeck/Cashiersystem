namespace Website.Models
{
    /// <summary>
    /// A stockItem
    /// </summary>
    public class StockItem
    {
        /// <summary>
        /// The Id of a stockitem
        /// </summary>
        public int StockItemId { get; set; }
        /// <summary>
        /// The name of the stockitem
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// FK to link a stockitem with a category
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// The unitprice of a stockitem
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// How many stockitems are in stock
        /// </summary>
        public int AvailableQuantity { get; set; }

    }
}
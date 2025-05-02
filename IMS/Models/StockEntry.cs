namespace IMS.Models
{
    public class StockEntry
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int Quantity { get; set; }  // Quantity of stock added
        public decimal Price { get; set; }  // Price at which the stock was purchased
        public DateTime EntryDate { get; set; }  // Date when the stock was added

        public string CreatedBy { get; set; }  // User who added the stock
        public DateTime CreatedAt { get; internal set; }
    }

}
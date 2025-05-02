namespace IMS.Models
{
    public class ProductInventory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int Quantity { get; set; }  // Total quantity in the warehouse

        // Link to stock entries (many-to-one relation with StockEntry)
        public virtual ICollection<StockEntry> StockEntries { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }


}
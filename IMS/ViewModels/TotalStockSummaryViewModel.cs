namespace IMS.ViewModels
{
    public class TotalStockSummaryViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalWarehouses { get; set; }
        public int TotalInventoryQuantity { get; set; }
        public int OutOfStockItems { get; set; }
    }
}

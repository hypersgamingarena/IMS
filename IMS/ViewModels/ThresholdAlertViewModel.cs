namespace IMS.ViewModels
{
    public class ThresholdAlertViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string WarehouseName { get; set; }
        public int CurrentQuantity { get; set; }
        public int Threshold { get; set; }
    }
}

using System;

namespace IMS.ViewModels
{
    public class VendorOrderViewModel
    {
        public int OrderId { get; set; }
        public string VendorName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

using IMS.ViewModels;

namespace IMS.ViewModels
{
    public class AdminDashboardViewModel
    {
        public TotalStockSummaryViewModel TotalStockSummary { get; set; }
        public List<ThresholdAlertViewModel> ThresholdAlerts { get; set; }
        public List<VendorOrderViewModel> PendingVendorOrders { get; set; }
    }
}

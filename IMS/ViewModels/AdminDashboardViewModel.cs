using IMS.ViewModels;

namespace IMS.ViewModels
{
    public class AdminDashboardViewModel
    {
        public TotalStockSummaryViewModel TotalStockSummary { get; set; }
        public List<ThresholdAlertViewModel> ThresholdAlerts { get; set; }
        public List<VendorOrderViewModel> PendingVendorOrders { get; set; }
        public AdminDashboardViewModel() { }
        public AdminDashboardViewModel(TotalStockSummaryViewModel totalStockSummary, List<ThresholdAlertViewModel> thresholdAlerts, List<VendorOrderViewModel> pendingVendorOrders)
        {
            TotalStockSummary = totalStockSummary;
            ThresholdAlerts = thresholdAlerts;
            PendingVendorOrders = pendingVendorOrders;
        }
        public void UpdateDashboard(TotalStockSummaryViewModel totalStockSummary, List<ThresholdAlertViewModel> thresholdAlerts, List<VendorOrderViewModel> pendingVendorOrders)
        {
            TotalStockSummary = totalStockSummary;
            ThresholdAlerts = thresholdAlerts;
            PendingVendorOrders = pendingVendorOrders;
        }
        public void ClearDashboard()
        {
            TotalStockSummary = null;
            ThresholdAlerts = null;
            PendingVendorOrders = null;
        }
        public void RefreshDashboard()
        {
            // Logic to refresh the dashboard data
            // This could involve re-fetching data from the database or API
            // For now, we'll just clear the existing data
            ClearDashboard();
        }
        public void ResetDashboard()
        {
            // Logic to reset the dashboard to its initial state
            // This could involve setting default values or re-initializing the dashboard
            TotalStockSummary = new TotalStockSummaryViewModel();
            ThresholdAlerts = new List<ThresholdAlertViewModel>();
            PendingVendorOrders = new List<VendorOrderViewModel>();
        }
        public void Dispose()
        {
            // Logic to dispose of any resources if necessary
            // For now, we'll just clear the dashboard data
            ClearDashboard();
        }
        public void InitializeDashboard()
        {
            // Logic to initialize the dashboard with default values
            // This could involve fetching initial data from the database or API
            TotalStockSummary = new TotalStockSummaryViewModel();
            ThresholdAlerts = new List<ThresholdAlertViewModel>();
            PendingVendorOrders = new List<VendorOrderViewModel>();
        }
        public void LoadDashboardData()
        {
            // Logic to load the dashboard data
            // This could involve fetching data from the database or API
            // For now, we'll just set some dummy data
            TotalStockSummary = new TotalStockSummaryViewModel
            {
                TotalProducts = 100,
                TotalWarehouses = 5,
                TotalInventoryQuantity = 1000,
                OutOfStockItems = 10
            };
            ThresholdAlerts = new List<ThresholdAlertViewModel>
            {
                new ThresholdAlertViewModel { ProductId = 1, ProductName = "Product A", WarehouseName = "Warehouse 1", CurrentQuantity = 5, Threshold = 10 },
                new ThresholdAlertViewModel { ProductId = 2, ProductName = "Product B", WarehouseName = "Warehouse 2", CurrentQuantity = 3, Threshold = 5 }
            };
            PendingVendorOrders = new List<VendorOrderViewModel>
            {
                new VendorOrderViewModel { OrderId = 1, VendorName = "Vendor A", OrderDate = DateTime.Now, Status = "Pending", TotalAmount = 100.00m },
                new VendorOrderViewModel { OrderId = 2, VendorName = "Vendor B", OrderDate = DateTime.Now, Status = "Pending", TotalAmount = 200.00m }
            };
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IMS.Services;
using IMS.Interfaces;
using IMS.ViewModels;

namespace IMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IVendorService _vendorService;
      

        public AdminController(
            IInventoryService inventoryService,
            IVendorService vendorService)
           
        {
            _inventoryService = inventoryService;
            _vendorService = vendorService;
        }

        // Dashboard
        public async Task<IActionResult> Index()
        {
            var totalInventory = await _inventoryService.GetTotalStockSummaryAsync();
            

            var dashboardVM = new AdminDashboardViewModel
            {
                TotalStockSummary = totalInventory,
               
                
            };

            return View(dashboardVM);
        }

        // Optional: redirect to product management
        public IActionResult ManageProducts()
        {
            return RedirectToAction("Index", "Product");
        }

        // Optional: redirect to warehouse management
        public IActionResult ManageWarehouses()
        {
            return RedirectToAction("Index", "Warehouse");
        }
    }
}

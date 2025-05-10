using Microsoft.AspNetCore.Mvc;
using IMS.Models;
using IMS.Services;
using System.Threading.Tasks;

namespace IMS.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;
        private readonly IPriceTrackingService _priceTrackingService;

        public WarehouseController(IProductService productService, IInventoryService inventoryService, IPriceTrackingService priceTrackingService)
        {
            _productService = productService;
            _inventoryService = inventoryService;
            _priceTrackingService = priceTrackingService;
        }

        public async Task<IActionResult> Index()
        {
            var inventory = await _inventoryService.GetInventoryAsync();
            return View(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(string productName, string sku, int quantity, int? threshold)
        {
            // Check if product exists
            var product = await _productService.GetProductBySkuAsync(sku);
            if (product == null)
            {
                // Create new product if it doesn't exist
                product = new Product { Name = productName, Sku = sku, Threshold = threshold ?? 0 };
                await _productService.AddProductAsync(product);
            }

            // Insert stock entry
            var stockEntry = new StockEntry { ProductId = product.Id, Quantity = quantity };
            await _inventoryService.AddStockEntryAsync(stockEntry);

            // Check and update inventory
            var inventory = await _inventoryService.GetInventoryByProductAndWarehouseAsync(product.Id, 1); // Assuming warehouse ID 1
            if (inventory != null)
            {
                inventory.Quantity += quantity;
                await _inventoryService.UpdateInventoryAsync(inventory);
            }
            else
            {
                inventory = new Inventory { ProductId = product.Id, WarehouseId = 1, Quantity = quantity };
                await _inventoryService.AddInventoryAsync(inventory);
            }

            // Check if price has changed and track it
            if (product.Price != null)
            {
                await _priceTrackingService.TrackPriceChangeAsync(product.Id, product.Price.Value);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> FulfillVendorOrder(int orderId)
        {
            await _inventoryService.FulfillVendorOrderAsync(orderId);
            return RedirectToAction(nameof(Index));
        }
    }
}

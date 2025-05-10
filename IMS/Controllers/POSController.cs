using Microsoft.AspNetCore.Mvc;
using IMS.Models;
using IMS.Services;
using System.Threading.Tasks;

namespace IMS.Controllers
{
    public class POSController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IInvoiceService _invoiceService;
        private readonly IRequestService _requestService;

        public POSController(IInventoryService inventoryService, IInvoiceService invoiceService, IRequestService requestService)
        {
            _inventoryService = inventoryService;
            _invoiceService = invoiceService;
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellProduct(int productId, int quantity, bool isVendor)
        {
            // Update inventory in real-time
            var inventory = await _inventoryService.GetInventoryByProductAndWarehouseAsync(productId, 1); // Assuming warehouse ID 1
            if (inventory == null || inventory.Quantity < quantity)
            {
                return BadRequest("Insufficient inventory.");
            }

            inventory.Quantity -= quantity;
            await _inventoryService.UpdateInventoryAsync(inventory);

            // Generate invoice
            if (isVendor)
            {
                await _invoiceService.GenerateVendorInvoiceAsync(productId, quantity);
            }
            else
            {
                await _invoiceService.GenerateWalkInInvoiceAsync(productId, quantity);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequest(int productId, int quantity)
        {
            // Submit a request to the Admin for product fulfillment
            await _requestService.SubmitRequestAsync(productId, quantity);
            return RedirectToAction(nameof(Index));
        }
    }
}

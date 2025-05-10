using IMS.Interfaces;
using IMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IMS.Services;

namespace IMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        // POST: api/Vendor
        [HttpPost]
        public async Task<IActionResult> AddVendor([FromBody] Vendor vendor)
        {
            if (vendor == null)
                return BadRequest(new { success = false, message = "Vendor data is null." });

            try
            {
                var newVendor = await _vendorService.AddVendorAsync(vendor);

                return CreatedAtAction(nameof(GetVendor), new { id = newVendor.Id }, newVendor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        // GET: api/Vendor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendor(int id)
        {
            try
            {
                var vendor = await _vendorService.GetVendorByIdAsync(id);
                if (vendor == null)
                    return NotFound(new { success = false, message = "Vendor not found." });

                return Ok(vendor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        // PUT: api/Vendor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, [FromBody] Vendor updatedVendor)
        {
            if (updatedVendor == null)
                return BadRequest(new { success = false, message = "Updated vendor data is null." });

            try
            {
                var existingVendor = await _vendorService.UpdateVendorAsync(id, updatedVendor);
                if (existingVendor == null)
                    return NotFound(new { success = false, message = "Vendor not found to update." });

                return Ok(existingVendor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        // DELETE: api/Vendor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                var result = await _vendorService.DeleteVendorAsync(id);
                if (!result)
                    return NotFound(new { success = false, message = "Vendor not found to delete." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _vendorService.GetVendorOrdersAsync();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> FulfillOrder(int orderId)
        {
            await _vendorService.FulfillOrderAsync(orderId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PurchaseHistory()
        {
            var history = await _vendorService.GetPurchaseHistoryAsync();
            return View(history);
        }
    }
}

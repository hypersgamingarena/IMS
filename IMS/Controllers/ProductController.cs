using IMS.Interfaces;
using IMS.Models;
using IMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic; // Added for IEnumerable
using System.Linq; // Added for .Select
using System.Threading.Tasks;

namespace IMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMemoryCache _memoryCache;

        // Cache key constants
        private const string AllProductsCacheKey = "AllProductsCacheKey";
        private static string GetProductCacheKey(int id) => $"ProductCacheKey_{id}";

        public ProductController(IProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                if (!_memoryCache.TryGetValue(AllProductsCacheKey, out IEnumerable<ProductViewModel> productViewModels))
                {
                    var products = await _productService.GetAllProductsAsync(); // Assuming this method exists in IProductService
                    if (products == null)
                    {
                        products = new List<Product>(); // Return empty list if null
                    }

                    productViewModels = products.Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description
                    }).ToList();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5)) // Example: Shorter for list
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                    _memoryCache.Set(AllProductsCacheKey, productViewModels, cacheEntryOptions);
                }
                return Ok(productViewModels);
            }
            catch (Exception ex)
            {
                // Log the exception (using ILogger would be better here)
                Console.WriteLine($"Error in GetAllProducts: {ex.Message}");
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                string cacheKey = GetProductCacheKey(id);
                if (!_memoryCache.TryGetValue(cacheKey, out ProductViewModel productViewModel))
                {
                    var product = await _productService.GetProductByIdAsync(id);
                    if (product == null)
                        return NotFound(new { success = false, message = $"Product with ID {id} not found." });

                    productViewModel = new ProductViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description
                    };

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                    _memoryCache.Set(cacheKey, productViewModel, cacheEntryOptions);
                }

                return Ok(productViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetProduct({id}): {ex.Message}");
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product) // Consider using a ProductCreateViewModel
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (product == null)
                    return BadRequest(new { success = false, message = "Product data is null." });

                var newProduct = await _productService.AddProductAsync(product);
                if (newProduct == null)
                {
                    return StatusCode(500, new { success = false, message = "Failed to create product." });
                }

                // Invalidate AllProducts cache
                _memoryCache.Remove(AllProductsCacheKey);

                // Optionally, cache the newly added product (as ProductViewModel)
                var productViewModel = new ProductViewModel
                {
                    Id = newProduct.Id,
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    Description = newProduct.Description
                };
                _memoryCache.Set(GetProductCacheKey(newProduct.Id), productViewModel, TimeSpan.FromMinutes(10));


                return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, productViewModel); // Return ViewModel
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddProduct: {ex.Message}");
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct) // Consider using a ProductUpdateViewModel
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedProduct.Id) // Basic check
            {
                return BadRequest(new { success = false, message = "Product ID mismatch." });
            }

            try
            {
                var existingProduct = await _productService.GetProductByIdAsync(id); // Check if product exists first
                if (existingProduct == null)
                    return NotFound(new { success = false, message = $"Product with ID {id} not found for update." });

                var resultProduct = await _productService.UpdateProductAsync(id, updatedProduct);
                // UpdateProductAsync should ideally return the updated product or confirm success.
                // Assuming it returns the updated product:

                if (resultProduct == null) // Should not happen if GetProductByIdAsync passed, unless concurrent delete
                    return NotFound(new { success = false, message = "Product disappeared during update or update failed." });

                // Update product in cache
                var productViewModel = new ProductViewModel
                {
                    Id = resultProduct.Id,
                    Name = resultProduct.Name,
                    Price = resultProduct.Price,
                    Description = resultProduct.Description
                };
                _memoryCache.Set(GetProductCacheKey(id), productViewModel, TimeSpan.FromMinutes(10));

                // Invalidate AllProducts cache
                _memoryCache.Remove(AllProductsCacheKey);

                return Ok(productViewModel); // Return ViewModel
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateProduct({id}): {ex.Message}");
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await _productService.GetProductByIdAsync(id);
                if (productToDelete == null)
                    return NotFound(new { success = false, message = $"Product with ID {id} not found for deletion." });

                var result = await _productService.DeleteProductAsync(id);
                if (!result) // If service explicitly returns false for failure (other than not found)
                    return StatusCode(500, new { success = false, message = $"Failed to delete product with ID {id}." });

                // Remove from individual product cache
                _memoryCache.Remove(GetProductCacheKey(id));
                // Invalidate AllProducts cache
                _memoryCache.Remove(AllProductsCacheKey);

                return NoContent(); // Standard for successful DELETE
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteProduct({id}): {ex.Message}");
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        // POST: api/Product/AddStock
        //[HttpPost("AddStock")]
        //public async Task<IActionResult> AddStock([FromBody] AddStockRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        if (request == null || request.Product == null)
        //            return BadRequest(new { success = false, message = "AddStock request or product data is null." });

        //        // Add or update product and stock
        //        // This service method might create a product if it doesn't exist, or update it if it does, then adds stock.
        //        var updatedProduct = await _productService.AddOrUpdateProductWithStockAsync(
        //            request.Product,
        //            request.WarehouseId,
        //            request.Quantity,
        //            request.Price, // Assuming this is purchase price for stock
        //            request.ThresholdQuantity
        //        );

        //        if (updatedProduct == null)
        //        {
        //            return StatusCode(500, new { success = false, message = "Failed to add or update product with stock." });
        //        }

        //        // Invalidate AllProducts cache
        //        _memoryCache.Remove(AllProductsCacheKey);

        //        // Update individual product cache (or add if new)
        //        var productViewModel = new ProductViewModel
        //        {
        //            Id = updatedProduct.Id,
        //            Name = updatedProduct.Name,
        //            Price = updatedProduct.Price, // This is product's selling price
        //            Description = updatedProduct.Description
        //        };
        //        _memoryCache.Set(GetProductCacheKey(updatedProduct.Id), productViewModel, TimeSpan.FromMinutes(10));

        //        return Ok(new { success = true, message = "Product and stock updated successfully.", productId = updatedProduct.Id });
        //    }
        //    catch (ArgumentException argEx) // Example of handling specific exceptions from service
        //    {
        //        return BadRequest(new { success = false, message = argEx.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error in AddStock: {ex.Message}");
        //        return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
        //    }
        //}
    }
}
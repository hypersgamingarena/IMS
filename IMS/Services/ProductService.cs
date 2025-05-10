using IMS.Models;
using IMS.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace IMS.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, IMemoryCache memoryCache, ILogger<ProductService> logger)
        {
            _context = context;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        // Add or update product with stock
        public async Task<Product> AddOrUpdateProductWithStockAsync(Product newProduct, int warehouseId, int quantity, decimal price, int thresholdQuantity)
        {
            try
            {
                // Step 1: Check if product exists in cache
                if (!_memoryCache.TryGetValue(newProduct.Name, out Product cachedProduct))
                {
                    // If not found in cache, fetch from DB
                    var existingProduct = await _context.Products
                        .FirstOrDefaultAsync(p => p.Name == newProduct.Name);

                    if (existingProduct == null)
                    {
                        // Product doesn't exist, create a new one
                        _context.Products.Add(newProduct);
                        await _context.SaveChangesAsync();
                        // Cache the new product
                        _memoryCache.Set(newProduct.Name, newProduct, TimeSpan.FromMinutes(30));
                    }
                    else
                    {
                        // Product exists, use existing product ID
                        newProduct.Id = existingProduct.Id;
                        _memoryCache.Set(existingProduct.Name, existingProduct, TimeSpan.FromMinutes(30));
                    }
                }
                else
                {
                    // Use the cached product if available
                    newProduct = cachedProduct;
                }

                // Step 2: Add stock entry
                var stockEntry = new StockEntry
                {
                    ProductId = newProduct.Id,
                    WarehouseId = warehouseId,
                    Quantity = quantity,
                    Price = price,
                    EntryDate = DateTime.Now
                };

                _context.StockEntries.Add(stockEntry);
                await _context.SaveChangesAsync();

                // Step 3: Update Product Inventory
                var productInventory = await _context.ProductInventories
                    .FirstOrDefaultAsync(pi => pi.ProductId == newProduct.Id && pi.WarehouseId == warehouseId);

                if (productInventory != null)
                {
                    // Update inventory quantity and timestamp
                    productInventory.Quantity += quantity;
                    productInventory.UpdatedAt = DateTime.Now;
                    _context.ProductInventories.Update(productInventory);
                }
                else
                {
                    // Create new inventory entry
                    var newInventory = new ProductInventory
                    {
                        ProductId = newProduct.Id,
                        WarehouseId = warehouseId,
                        Quantity = quantity,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    _context.ProductInventories.Add(newInventory);
                }

                // Step 4: Handle price updates and add to ProductPriceTracking if necessary
                var existingProductInDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == newProduct.Id);
                if (existingProductInDb != null && existingProductInDb.Price != price)
                {
                    var priceTracking = new ProductPriceTracking
                    {
                        ProductId = newProduct.Id,
                        Price = price,
                        PriceUpdateDate = DateTime.Now,
                        UpdatedBy = "Admin" // Replace with dynamic user if needed
                    };

                    _context.ProductPriceTrackings.Add(priceTracking);
                }

                await _context.SaveChangesAsync();
                return newProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding or updating product: {ex.Message}", ex);
                throw new ApplicationException("There was an issue adding or updating the product.", ex);
            }
        }

        // Get product by ID with caching
        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                if (!_memoryCache.TryGetValue(id, out Product product))
                {
                    product = await _context.Products
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (product != null)
                    {
                        _memoryCache.Set(id, product, TimeSpan.FromMinutes(10)); // Cache product for 10 minutes
                    }
                }
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching product by ID: {ex.Message}", ex);
                throw new ApplicationException("Error occurred while retrieving the product.", ex);
            }
        }

        // Additional methods (just structure them for completion)

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching all products: {ex.Message}", ex);
                throw new ApplicationException("Error occurred while retrieving the products list.", ex);
            }
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            try
            {
                return await _context.Products
                    .FirstOrDefaultAsync(p => p.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching product by name: {ex.Message}", ex);
                throw new ApplicationException("Error occurred while retrieving the product by name.", ex);
            }
        }

        public async Task<Product> UpdateProductAsync(int id, Product updatedProduct)
        {
            try
            {
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }

                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Description = updatedProduct.Description;
                // Any other fields to update...

                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();

                // Clear cache as the product has been updated
                _memoryCache.Remove(existingProduct.Name);
                _memoryCache.Set(existingProduct.Name, existingProduct, TimeSpan.FromMinutes(10));

                return existingProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating product: {ex.Message}", ex);
                throw new ApplicationException("Error occurred while updating the product.", ex);
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                // Remove product from cache
                _memoryCache.Remove(product.Name);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting product: {ex.Message}", ex);
                throw new ApplicationException("Error occurred while deleting the product.", ex);
            }
        }

        public Task<Product> AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> AddOrUpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductBySKUAsync(string sku)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsBySupplierAsync(string supplier)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByWarehouseAsync(int warehouseId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        Task IProductService.AddOrUpdateProductWithStockAsync(Product product, int warehouseId, int quantity, decimal price, int thresholdQuantity)
        {
            return AddOrUpdateProductWithStockAsync(product, warehouseId, quantity, price, thresholdQuantity);
        }

        // Other necessary methods here...
    }
}

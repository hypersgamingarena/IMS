using IMS.Interfaces;
using IMS.Models;
using Microsoft.EntityFrameworkCore;


    namespace IMS.Services
    {
        public class ProductService : IProductService
        {
            private readonly ApplicationDbContext _context;

            public ProductService(ApplicationDbContext context)
            {
                _context = context;
            }

            // CREATE: Add a new product
            public async Task<Product> AddProductAsync(Product product)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }

            // READ: Get a product by its ID
            public async Task<Product> GetProductByIdAsync(int id)
            {
                return await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id);  // Fetch product by ID
            }

            // READ: Get all products
            public async Task<List<Product>> GetAllProductsAsync()
            {
                return await _context.Products
                    .ToListAsync();  // Return a list of all products
            }

            // UPDATE: Update an existing product
            public async Task<Product> UpdateProductAsync(int id, Product updatedProduct)
            {
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct != null)
                {
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.Price = updatedProduct.Price;
                    existingProduct.Description = updatedProduct.Description;
                    existingProduct.UpdatedAt = DateTime.Now;

                    _context.Products.Update(existingProduct);
                    await _context.SaveChangesAsync();
                }
                return existingProduct;
            }

            // DELETE: Delete a product
            public async Task<bool> DeleteProductAsync(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }
    }



using IMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMS.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> UpdateProductAsync(int id, Product updatedProduct);
        Task<bool> DeleteProductAsync(int id);
        Task<Product> AddOrUpdateProductAsync(Product product);
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> GetProductBySKUAsync(string sku);
        Task<List<Product>> GetProductsByCategoryAsync(string category);
        Task<List<Product>> GetProductsBySupplierAsync(string supplier);
        Task<List<Product>> GetProductsByWarehouseAsync(int warehouseId);
        Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);

        Task AddOrUpdateProductWithStockAsync(Product product, int warehouseId, int quantity, decimal price, int thresholdQuantity);
    }
}

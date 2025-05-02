using System.Threading.Tasks;
using IMS.Models;
using IMS.ViewModels;

namespace IMS.Interfaces
{
    
        public interface IInventoryService
        {
            Task<ProductInventory> AddInventoryAsync(ProductInventory inventory);
            Task<ProductInventory> GetInventoryAsync(int productId, int warehouseId);
            Task<ProductInventory> UpdateInventoryAsync(int id, ProductInventory updatedInventory);
            Task<bool> DeleteInventoryAsync(int id);
            Task<List<ProductInventory>> GetAllInventoriesAsync();
        Task<TotalStockSummaryViewModel> GetTotalStockSummaryAsync();
    }

    
}

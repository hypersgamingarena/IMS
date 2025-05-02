using IMS.Models;

namespace IMS.Interfaces
{
    public interface IStockEntryService
    {
        Task<StockEntry> AddStockEntryAsync(StockEntry stockEntry);
        Task<StockEntry> GetStockEntryByIdAsync(int id);
        Task<List<StockEntry>> GetStockEntriesByProductAndWarehouseAsync(int productId, int warehouseId);
        Task<StockEntry> UpdateStockEntryAsync(int id, StockEntry updatedStockEntry);
        Task<bool> DeleteStockEntryAsync(int id);
        Task<List<StockEntry>> GetAllStockEntriesAsync();
    }

}

using IMS.Interfaces;
using IMS.Models;
using IMS.ViewModels;
using Microsoft.EntityFrameworkCore;

public class InventoryService : IInventoryService
{
    private readonly ApplicationDbContext _context;

    public InventoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task<ProductInventory> AddInventoryAsync(ProductInventory inventory)
    {
        _context.ProductInventories.Add(inventory);
        await _context.SaveChangesAsync();
        return inventory;
    }

    // READ (Get product inventory by ProductId and WarehouseId)
    public async Task<ProductInventory> GetInventoryAsync(int productId, int warehouseId)
    {
        return await _context.ProductInventories
            .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.WarehouseId == warehouseId);
    }

    // UPDATE (Update the inventory)
    public async Task<ProductInventory> UpdateInventoryAsync(int id, ProductInventory updatedInventory)
    {
        var existingInventory = await _context.ProductInventories.FindAsync(id);
        if (existingInventory != null)
        {
            existingInventory.Quantity = updatedInventory.Quantity;
            existingInventory.UpdatedAt = DateTime.Now;
            _context.ProductInventories.Update(existingInventory);
            await _context.SaveChangesAsync();
        }
        return existingInventory;
    }

    // DELETE (Delete a product inventory record)
    public async Task<bool> DeleteInventoryAsync(int id)
    {
        var inventory = await _context.ProductInventories.FindAsync(id);
        if (inventory != null)
        {
            _context.ProductInventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    // GET All Inventories (Example: For listing all inventories)
    public async Task<List<ProductInventory>> GetAllInventoriesAsync()
    {
        return await _context.ProductInventories.ToListAsync();
    }

    public Task<TotalStockSummaryViewModel> GetTotalStockSummaryAsync()
    {
        throw new NotImplementedException();
    }
}

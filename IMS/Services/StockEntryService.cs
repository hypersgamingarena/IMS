using IMS.Models;
using Microsoft.EntityFrameworkCore;


namespace IMS.Services
{
    public class StockEntryService
    {
        private readonly ApplicationDbContext _context;

        public StockEntryService(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE: Add a new stock entry
        public async Task<StockEntry> AddStockEntryAsync(StockEntry stockEntry)
        {
            _context.StockEntries.Add(stockEntry);
            await _context.SaveChangesAsync();  // Save changes to the database
            return stockEntry;  // Return the added stock entry
        }

        // READ: Get a stock entry by its ID
        public async Task<StockEntry> GetStockEntryByIdAsync(int id)
        {
            return await _context.StockEntries
                .FirstOrDefaultAsync(se => se.Id == id);  // Get the stock entry by ID
        }

        // READ: Get stock entries for a specific product and warehouse
        public async Task<List<StockEntry>> GetStockEntriesByProductAndWarehouseAsync(int productId, int warehouseId)
        {
            return await _context.StockEntries
                .Where(se => se.ProductId == productId && se.WarehouseId == warehouseId)  // Filter by ProductId and WarehouseId
                .OrderByDescending(se => se.EntryDate)  // Optional: Order by EntryDate (latest first)
                .ToListAsync();
        }

        // UPDATE: Update an existing stock entry
        public async Task<StockEntry> UpdateStockEntryAsync(int id, StockEntry updatedStockEntry)
        {
            var existingStockEntry = await _context.StockEntries.FindAsync(id);
            if (existingStockEntry != null)
            {
                existingStockEntry.Quantity = updatedStockEntry.Quantity;  // Update quantity
                existingStockEntry.Price = updatedStockEntry.Price;        // Update price
                existingStockEntry.EntryDate = updatedStockEntry.EntryDate;  // Update entry date
                existingStockEntry.CreatedBy = updatedStockEntry.CreatedBy;  // Update who created
                existingStockEntry.CreatedAt = DateTime.Now;  // Set updated timestamp

                _context.StockEntries.Update(existingStockEntry);
                await _context.SaveChangesAsync();  // Save the changes to the database
            }
            return existingStockEntry;  // Return the updated stock entry
        }

        // DELETE: Delete a stock entry by ID
        public async Task<bool> DeleteStockEntryAsync(int id)
        {
            var stockEntry = await _context.StockEntries.FindAsync(id);
            if (stockEntry != null)
            {
                _context.StockEntries.Remove(stockEntry);  // Remove the stock entry
                await _context.SaveChangesAsync();  // Save the changes to the database
                return true;
            }
            return false;  // Return false if no entry was found
        }

        // READ: Get all stock entries (optional, for listing all stock entries)
        public async Task<List<StockEntry>> GetAllStockEntriesAsync()
        {
            return await _context.StockEntries
                .OrderByDescending(se => se.EntryDate)  // Optional: Order by EntryDate (latest first)
                .ToListAsync();
        }
    }

}

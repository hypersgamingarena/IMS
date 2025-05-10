using IMS.Interfaces;
using Microsoft.EntityFrameworkCore;
using IMS.Models;

namespace IMS.Services
{
    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext _context;

        public VendorService(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE (Add a new vendor)
        public async Task<Vendor> AddVendorAsync(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return vendor;
        }

        // READ (Get vendor by ID)
        public async Task<Vendor> GetVendorAsync(int id)
        {
            return await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
        }

        // UPDATE (Update a vendor's details)
        public async Task<Vendor> UpdateVendorAsync(int id, Vendor updatedVendor)
        {
            var existingVendor = await _context.Vendors.FindAsync(id);
            if (existingVendor != null)
            {
                existingVendor.Name = updatedVendor.Name;
                existingVendor.ContactInfo = updatedVendor.ContactInfo;
                _context.Vendors.Update(existingVendor);
                await _context.SaveChangesAsync();
            }
            return existingVendor;
        }

        // DELETE (Delete a vendor)
        public async Task<bool> DeleteVendorAsync(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<Vendor> GetVendorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> GetVendorByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }

}

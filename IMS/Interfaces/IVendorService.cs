using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using IMS.Models;
using IMS.ViewModels;

namespace IMS.Interfaces
{
    public interface IVendorService
    {
        Task<Vendor> AddVendorAsync(Vendor vendor);
        Task<Vendor> GetVendorAsync(int id);
        Task<Vendor> UpdateVendorAsync(int id, Vendor updatedVendor);
        Task<bool> DeleteVendorAsync(int id);
    }

}

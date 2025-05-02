using IMS.Models;

namespace IMS.Models;

public class Vendor
{
    public int Id { get; set; }
    public string Name { get; set; }   // Vendor's name
    public string ContactInfo { get; set; }  // Contact information (phone, email, etc.)
    public string Address { get; set; }  // Vendor's address
    public string VendorCode { get; set; }  // Vendor code (optional, can be used for internal tracking)

    // DateTime for tracking creation and updates
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Tracking who created or updated the vendor record
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }

    // Vendor active or deleted status
    public bool IsActive { get; set; }  // Whether the vendor is active or not
    public bool IsDeleted { get; set; } // Soft delete flag

    // Navigation property for Vendor's Products (if applicable)
    public virtual ICollection<Product> ProductsSupplied { get; set; }  // Products supplied by the vendor

    // Navigation property for Vendor's Orders (if applicable)
    public virtual ICollection<Order> Orders { get; set; }  // Orders associated with the vendor
}

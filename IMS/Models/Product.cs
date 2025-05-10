namespace IMS.Models;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }   // Product name
    public string SKU { get; set; }    // Product SKU
    public string Description { get; set; } // Description of the product


    // Price and Threshold directly in Product model
    public decimal Price { get; set; }  // Current price of the product
    public int ThresholdQuantity { get; set; } // Threshold quantity for the product

    // Parent-Child relationship for products
    public int? ParentProductId { get; set; }
    public virtual Product ParentProduct { get; set; }
    public virtual ICollection<Product> SubProducts { get; set; }

    // DateTime for tracking creation and updates
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Tracking user info for who created or updated the product
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }

    // Product active or deleted status
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }


    // Navigation properties for stock entries, price tracking, and inventory
    public virtual ICollection<StockEntry> StockEntries { get; set; }
    public virtual ICollection<ProductPriceTracking> ProductPriceTrackings { get; set; }
    public virtual ICollection<ProductInventory> ProductInventories { get; set; }
    public virtual ICollection<ProductImage> ProductImages { get; set; }  // Link to product images
}

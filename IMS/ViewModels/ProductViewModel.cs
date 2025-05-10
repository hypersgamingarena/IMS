using System.ComponentModel.DataAnnotations;

namespace IMS.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        // Add additional fields if required
        public int StockQuantity { get; set; } // Example: Stock quantity
        public string Category { get; set; } // Example: Product category
        public string SKU { get; set; } // Example: Stock Keeping Unit
        public string Supplier { get; set; } // Example: Product supplier
        public string Warehouse { get; set; } // Example: Product warehouse
        public DateTime CreatedAt { get; set; } // Example: Creation date
        public DateTime UpdatedAt { get; set; } // Example: Last updated date
        public string CreatedBy { get; set; } // Example: User who created the product
        public string UpdatedBy { get; set; } // Example: User who last updated the product
        public bool IsActive { get; set; } // Example: Product status (active/inactive)
        public string ImageUrl { get; set; } // Example: URL of the product image
        //public string Barcode { get; set; } // Example: Product barcode
        //public string QRCode { get; set; } // Example: Product QR code
        //public string Tags { get; set; } // Example: Product tags for search
        //public string Notes { get; set; } // Example: Additional notes about the product
        //public string Manufacturer { get; set; } // Example: Product manufacturer
        //public string Warranty { get; set; } // Example: Product warranty information
    }
}

namespace IMS.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }

        // Soft delete flag for image
        public bool IsDeleted { get; set; }  // Marks the image as deleted without removing it from the database

        // Foreign key for Product
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }


}

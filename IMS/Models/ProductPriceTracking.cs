namespace IMS.Models
{
    public class ProductPriceTracking
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }  // Link to the product

        public decimal Price { get; set; }  // Price of the product at the time of update
        public DateTime PriceUpdateDate { get; set; }  // Date when the price was updated

        public string UpdatedBy { get; set; }  // User who updated the price
    }

}
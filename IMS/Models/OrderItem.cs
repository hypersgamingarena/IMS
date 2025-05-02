using IMS.Models;

namespace IMS.Models;
public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }  // Quantity of the product ordered
    public decimal Price { get; set; }  // Price at the time of ordering
    public decimal TotalPrice { get; set; }  // Total price for this item (Quantity * Price)

    // Foreign Key for Product (Which product was ordered)
    public int ProductId { get; set; }
    public Product Product { get; set; }

    // Foreign Key for Order (Which order this item belongs to)
    public int OrderId { get; set; }
    public Order Order { get; set; }
}

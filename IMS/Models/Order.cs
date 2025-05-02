using IMS.Models;
namespace IMS.Models;
public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } // The date the order was placed
    public decimal TotalAmount { get; set; } // Total amount of the order

    // Foreign Key for ApplicationUser (Customer who placed the order)
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    // Relationship with OrderItems (One order can have multiple order items)
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    // Status of the order (e.g., Pending, Shipped, Delivered)
    public string OrderStatus { get; set; } // Status of the order

    // Tracking who created or updated the order
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public virtual Invoice Invoice { get; set; } // Relationship with Invoice (One order can have one invoice)
}

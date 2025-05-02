using IMS.Models;

namespace IMS.Models;
public class Invoice
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } // Unique identifier for the invoice
    public DateTime InvoiceDate { get; set; } // Date the invoice was created
    public decimal TotalAmount { get; set; }  // Total amount for the invoice
    public string PaymentStatus { get; set; } // Payment status (e.g., Paid, Unpaid, Pending)

    // Foreign Key for Order (The order that the invoice is linked to)
    public int OrderId { get; set; }
    public Order Order { get; set; }

    // Tracking who created or updated the invoice
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
}

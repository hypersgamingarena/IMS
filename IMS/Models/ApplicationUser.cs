using Microsoft.AspNetCore.Identity;
namespace IMS.Models;
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } // Full Name of the user
    public string Address { get; set; } // Address of the user
    public string PhoneNumber { get; set; } // Phone number of the user

    // Date and time when the user was created
    public DateTime CreatedAt { get; set; }

    // Tracking who created or updated the user record
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }

    // Relationship with Orders (A user can have multiple orders)
    public virtual ICollection<Order> Orders { get; set; }
}

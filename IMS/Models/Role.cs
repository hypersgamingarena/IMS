using Microsoft.AspNetCore.Identity;

namespace IMS.Models;
public class Role : IdentityRole
{
    // You can add custom properties if needed, but IdentityRole provides the basic functionality
    public string Description { get; set; } // Description of the role (optional)
}

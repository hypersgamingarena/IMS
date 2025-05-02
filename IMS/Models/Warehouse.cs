using IMS.Models;

namespace IMS.Models;
public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; }    // Warehouse name
    public string Location { get; set; } // Warehouse location

    // Link to inventories for the products in this warehouse
    public virtual ICollection<ProductInventory> ProductInventories { get; set; }
}

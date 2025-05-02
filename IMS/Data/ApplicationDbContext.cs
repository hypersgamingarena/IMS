using IMS.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<StockEntry> StockEntries { get; set; }
    public DbSet<ProductInventory> ProductInventories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ProductPriceTracking> ProductPriceTrackings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure 1-to-many relationship between Product and ProductImage
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductImages)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);  // Delete related images when a product is deleted

        // Configure many-to-many relationship between Product and Warehouse
        modelBuilder.Entity<ProductInventory>()
            .HasKey(pi => new { pi.ProductId, pi.WarehouseId });

        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductInventories)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletes on ProductInventory

        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Warehouse)
            .WithMany(w => w.ProductInventories)
            .HasForeignKey(pi => pi.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletes on ProductInventory

        // Configure the 1-to-many relationship between Order and OrderItems
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);  // Delete related order items when an order is deleted

        // Configure the 1-to-1 relationship between Order and Invoice
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Invoice)
            .WithOne(i => i.Order)
            .HasForeignKey<Invoice>(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);  // Delete the related invoice when the order is deleted

        // Configure the 1-to-many relationship between ApplicationUser and Order
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.ApplicationUser)
            .HasForeignKey(o => o.ApplicationUserId)
            .OnDelete(DeleteBehavior.SetNull);  // Set null on delete if ApplicationUser is deleted

        // Ensure ApplicationUserId is nullable for SetNull behavior
        modelBuilder.Entity<Order>()
            .Property(o => o.ApplicationUserId)
            .IsRequired(false);  // Make sure the ApplicationUserId is nullable in the database
    }
}

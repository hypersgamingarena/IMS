using IMS.Interfaces;
using IMS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Register services for dependency injection
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IWhatsAppService, WhatsAppService>();

// Add DbContext (SQLite connection in this case)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MemoryCache services
builder.Services.AddMemoryCache();  // Add memory caching services

// Add controllers to the services collection
builder.Services.AddControllers();

var app = builder.Build();

// Configure middleware
app.UseRouting();  // Enable routing
app.UseAuthorization();  // Enable authorization

// Map controllers to handle requests
app.MapControllers();

app.Run();

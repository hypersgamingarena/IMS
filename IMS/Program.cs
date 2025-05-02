using IMS.Interfaces;
using IMS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register services for dependency injection
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IProductService, ProductService>();  // Register ProductService and its interface
builder.Services.AddScoped<IVendorService, VendorService>();

// Add DbContext (SQLite connection in this case)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers to the services collection
builder.Services.AddControllers();

var app = builder.Build();

// Configure middleware
app.UseRouting();  // Enable routing
app.UseAuthorization();  // Enable authorization

app.MapControllers();  // Map the controllers to handle requests

app.Run();

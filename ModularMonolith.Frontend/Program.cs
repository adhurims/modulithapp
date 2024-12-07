using Microsoft.AspNetCore.Identity;
using CustomerManagement.Infrastructure.Persistence;
using CustomerManagement.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Frontend.Services;
using Inventory.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Mappings;
using Ordering.Application.Services;
using Ordering.Domain.DomainServices;
using Ordering.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers with Views
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Configure CustomerManagementDbContext
builder.Services.AddDbContext<CustomerManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerManagementConnection")));

// Configure Identity - Avoid Duplicate Registrations
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CustomerManagementDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IPaymentService, StripePaymentService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stripeApiKey = configuration["Stripe:ApiKey"]; // Lexo çelësin nga konfigurimi
    return new StripePaymentService(stripeApiKey);
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status400BadRequest));
});

builder.Services.AddAutoMapper(typeof(ProductProfile));

// Configure HttpClient Services
builder.Services.AddHttpClient<CustomerService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5154/");
});

builder.Services.AddHttpClient<OrderingService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5154/");
});

builder.Services.AddHttpClient<InventoryService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5154/");
});

// Add SeedRoles to DI
builder.Services.AddScoped<SeedRoles>();

// Build the app
var app = builder.Build();

// Role and Data Seeding
using (var scope = app.Services.CreateScope())
{
    var seedRoles = scope.ServiceProvider.GetRequiredService<SeedRoles>();
    await seedRoles.SeedAsync();

    var services = scope.ServiceProvider;
    await DataSeeder.SeedAsync(services);
}

// Error Handling Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware Pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

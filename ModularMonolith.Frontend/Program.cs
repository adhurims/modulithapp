using CustomerManagement.Infrastructure.Persistence; 
using Ordering.Infrastructure.Persistence; 
using CustomerManagement.Infrastructure.Repositories;  
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Repositories;
using CustomerManagement.Application.Mappings; 
using Ordering.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using CustomerManagement.Domain.Repositories;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Repositories;
using Inventory.Application.Mappings;
using Inventory.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddDbContext<CustomerManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerManagementConnection")));

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryConnection")));

builder.Services.AddDbContext<OrderingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderingConnection")));
 
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(CustomerProfile), typeof(ProductProfile), typeof(OrderProfile));
 
//builder.Services.AddMediatR(typeof(CustomerManagement.Application.Services.CustomerService));
//builder.Services.AddMediatR(typeof(Inventory.Application.Services.ProductService));
//builder.Services.AddMediatR(typeof(Ordering.Application.Services.OrderService));



var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.Run();

using Microsoft.AspNetCore.Identity;
using CustomerManagement.Infrastructure.Persistence; // DbContext
using CustomerManagement.Domain.Identity; // ApplicationUser
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Frontend.Services; // Add necessary namespaces

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CustomerManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerManagementConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CustomerManagementDbContext>()
    .AddDefaultTokenProviders();

// Configure application cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Seed the database with default user and role
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedAsync(services);
}

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

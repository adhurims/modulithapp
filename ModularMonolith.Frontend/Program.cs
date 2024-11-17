using Microsoft.AspNetCore.Identity;
using CustomerManagement.Infrastructure.Persistence; 
using CustomerManagement.Domain.Identity;  
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Frontend.Services;  

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllersWithViews();
 
builder.Services.AddDbContext<CustomerManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerManagementConnection")));
 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CustomerManagementDbContext>()
    .AddDefaultTokenProviders();
 
builder.Services.AddHttpClient<CustomerService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5154/");  
});
 
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  
    options.SlidingExpiration = true;
});
 
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedAsync(services);  
}
  
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

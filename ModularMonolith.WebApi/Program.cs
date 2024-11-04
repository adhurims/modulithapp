using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using CustomerManagement.Infrastructure.Persistence;
using CustomerManagement.Domain.Identity;
using CustomerManagement.Application.Mappings;
using CustomerManagement.Domain.Repositories;
using CustomerManagement.Infrastructure.Repositories;
using Inventory.Application.Mappings;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Repositories;
using Ordering.Application.Mappings;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddDbContext<CustomerManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerManagementConnection")));

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryConnection")));

builder.Services.AddDbContext<OrderingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderingConnection")));
 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CustomerManagementDbContext>()
    .AddDefaultTokenProviders();
 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
 
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
 
builder.Services.AddAutoMapper(typeof(CustomerProfile), typeof(ProductProfile), typeof(OrderProfile));
 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ModularMonolith API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer prefix",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
 
builder.Services.AddControllers();

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ModularMonolith API v1"));
}
 
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();

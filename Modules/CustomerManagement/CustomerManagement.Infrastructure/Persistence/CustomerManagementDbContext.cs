using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Persistence
{
    public class CustomerManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Customer-ApplicationUser relationship
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Customer primary key
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);

            // Configure properties
            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Address)
                .HasMaxLength(200);
        }
    }
}

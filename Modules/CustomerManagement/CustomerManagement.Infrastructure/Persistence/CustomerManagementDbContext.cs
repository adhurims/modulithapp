using CustomerManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Infrastructure.Persistence
{
    public class CustomerManagementDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(100);
            modelBuilder.Entity<Customer>().Property(c => c.Address).HasMaxLength(200);
        }
    }
}

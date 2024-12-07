using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderingDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Order entity
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items) // Navigation property in Order
                .WithOne(oi => oi.Order) // Navigation property in OrderItem
                .HasForeignKey(oi => oi.OrderId) // Foreign key in OrderItem
                .OnDelete(DeleteBehavior.Cascade);

            // Configure OrderItem entity
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId }); // Composite key

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Quantity)
                .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order) // Navigation property in OrderItem
                .WithMany(o => o.Items) // Navigation property in Order
                .HasForeignKey(oi => oi.OrderId); // Foreign key in OrderItem
        }

    }
}

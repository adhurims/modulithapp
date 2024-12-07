using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc; 
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace Ordering.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; } // Unique ID for the order

        public DateTime OrderDate { get; set; } // Date of the order

        public decimal TotalAmount { get; set; } // Total amount for the order

        public string PaymentStatus { get; set; } // Status of the payment (e.g., "Pending", "Completed")

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>(); // List of items in the order
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; } // ID of the product

        public string ProductName { get; set; } // Name of the product (optional, if needed)

        public int Quantity { get; set; } // Quantity ordered

        public decimal Price { get; set; } // Price per item

        public decimal TotalPrice => Quantity * Price; // Calculated total price for the item
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } // Opsionale për stokun aktual
        public object ProductId { get; set; }
    }

    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Order items are required.")]
        [MinLength(1, ErrorMessage = "An order must have at least one item.")] 
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public List<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
        public IEnumerable<SelectListItem> ProductList { get; set; } // Correct namespace

        [Required(ErrorMessage = "Total amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        public string PaymentMethod { get; set; } // Example: "Card", "Cash", etc.

    }

    public class CreateOrderItemDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
    }

    public class UpdateOrderDto
    {
        public DateTime OrderDate { get; set; } // Data e re e porosisë
        public List<UpdateOrderItemDto> Items { get; set; } = new List<UpdateOrderItemDto>(); // Lista e artikujve të përditësuar
    }

    public class UpdateOrderItemDto
    {
        public int ProductId { get; set; } // ID e produktit
        public int Quantity { get; set; } // Sasia e re
        public decimal Price { get; set; } // Çmimi i ri
    }
}

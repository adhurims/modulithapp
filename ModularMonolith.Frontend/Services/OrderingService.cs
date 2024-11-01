using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ModularMonolith.Frontend.Models;
using Ordering.Application.DTOs;

namespace ModularMonolith.Frontend.Services
{
    public class OrderingService
    {
        private readonly HttpClient _httpClient;

        public OrderingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all orders
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OrderDto>>("api/ordering");
        }

        // Get a single order by ID
        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<OrderDto>($"api/ordering/{id}");
        }

        // Create a new order
        public async Task CreateOrderAsync(OrderDto order)
        {
            await _httpClient.PostAsJsonAsync("api/ordering", order);
        }

        // Update an existing order
        public async Task UpdateOrderAsync(int id, OrderDto order)
        {
            await _httpClient.PutAsJsonAsync($"api/ordering/{id}", order);
        }

        // Delete an order by ID
        public async Task DeleteOrderAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/ordering/{id}");
        }
    }
}

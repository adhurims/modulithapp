using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Inventory.Application.DTOs;
using Ordering.Application.DTOs;
using ProductDto = Ordering.Application.DTOs.ProductDto;

namespace ModularMonolith.Frontend.Services
{
    public class OrderingService
    {
        private readonly HttpClient _httpClient;

        public OrderingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Krijimi i një porosie të re
        public async Task CreateOrderAsync(CreateOrderDto order)
        {
            await _httpClient.PostAsJsonAsync("api/ordering", order);
        }

        // Lista e të gjitha porosive
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var response = await _httpClient.GetAsync("api/ordering");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {errorMessage}");
            }

            return await response.Content.ReadFromJsonAsync<List<OrderDto>>();
        }

        // Porosia sipas ID-së
        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<OrderDto>($"api/ordering/{id}");
        }

        // Përditësimi i një porosie
        public async Task UpdateOrderAsync(int id, UpdateOrderDto order)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ordering/{id}", order);
            response.EnsureSuccessStatusCode();
        }

        // Fshirja e një porosie
        public async Task DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ordering/{id}");
            response.EnsureSuccessStatusCode();
        }

        // Procesimi i pagesës
        public async Task<PaymentResult> ProcessPaymentAsync(CreateOrderDto order)
        {
            // Krijimi i një kërkese për pagesë
            var response = await _httpClient.PostAsJsonAsync("api/payments", order);
            if (response.IsSuccessStatusCode)
            {
                return new PaymentResult { IsSuccess = true };
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return new PaymentResult
            {
                IsSuccess = false,
                ErrorMessage = errorContent
            };
        }

        public async Task<List<ProductDto>> GetAvailableProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/ordering");
            return response ?? new List<ProductDto>();
        }


        // Lista e produkteve për porosi
        //public async Task<List<ProductDto>> GetAvailableProductsAsync()
        //{
        //    return await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/ordering");
        //}
    }

    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}

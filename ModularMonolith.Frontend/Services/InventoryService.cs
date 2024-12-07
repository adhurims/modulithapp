// Services/InventoryService.cs
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Inventory.Application.DTOs;
using ModularMonolith.Frontend.Models;

namespace ModularMonolith.Frontend.Services
{

    public class InventoryService
    {
        private readonly HttpClient _httpClient;

        public InventoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
         
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/product");
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/product/{id}");
        }

        // Create a new product
        public async Task CreateProductAsync(ProductDto product)
        {
            await _httpClient.PostAsJsonAsync("api/product", product);
        }

        // Update an existing product
        public async Task UpdateProductAsync(int id, ProductDto product)
        {
            await _httpClient.PutAsJsonAsync($"api/product/{id}", product);
        }

        // Delete a product by ID
        public async Task DeleteProductAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/product/{id}");
        }
    }
}


using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ModularMonolith.Frontend.Models;

namespace ModularMonolith.Frontend.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            var response = await _httpClient.GetAsync("api/customer");  
            response.EnsureSuccessStatusCode();   
            return await response.Content.ReadFromJsonAsync<List<CustomerModel>>();
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/customer/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerModel>();
        }

        public async Task AddCustomerAsync(CustomerModel customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customer", customer);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCustomerAsync(int id, CustomerModel customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customer/{id}", customer);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/customer/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

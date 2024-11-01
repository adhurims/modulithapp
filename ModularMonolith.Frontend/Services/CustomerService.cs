namespace ModularMonolith.Frontend.Services
{
    // Services/CustomerService.cs
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using CustomerManagement.Application.DTOs; 

    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customer");
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customer/{id}");
        }

        public async Task CreateCustomerAsync(CustomerDto customer)
        {
            await _httpClient.PostAsJsonAsync("api/customer", customer);
        }

        public async Task UpdateCustomerAsync(int id, CustomerDto customer)
        {
            await _httpClient.PutAsJsonAsync($"api/customer/{id}", customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/customer/{id}");
        }
    }

}

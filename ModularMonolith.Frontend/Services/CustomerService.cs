using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerManagement.Domain.Entities;
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
            var customer = await _httpClient.GetFromJsonAsync<CustomerModel>($"api/customer/{id}");
            return customer;
        }

        //public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        //{
        //    var response = await _httpClient.GetAsync($"api/customer/{id}");
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadFromJsonAsync<CustomerModel>();
        //}

        public async Task AddCustomerAsync(Customer customer)
        {
             
            //customer.UserId = "defaultUserId";  
            var response = await _httpClient.PostAsJsonAsync("api/customer", customer);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode}, Details: {errorContent}");
            }
        }

        public async Task UpdateCustomerAsync(int id, CustomerModel customer)
        {
            if (string.IsNullOrEmpty(customer.UserId))
            {
                throw new Exception("UserId is required for updating a customer.");
            }

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

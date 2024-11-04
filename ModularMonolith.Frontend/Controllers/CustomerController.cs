using CustomerManagement.Application.DTOs;
using CustomerManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Frontend.Models;
using ModularMonolith.Frontend.Services;
using System.Threading.Tasks;

namespace ModularMonolith.Frontend.Controllers
{ 
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _httpClient.GetFromJsonAsync<List<Customer>>("api/customer");
            return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customer", customer);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "An error occurred while creating the customer.");
            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"api/customer/{id}");
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customer/{id}", customer);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "An error occurred while updating the customer.");
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"api/customer/{id}");
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/customer/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}

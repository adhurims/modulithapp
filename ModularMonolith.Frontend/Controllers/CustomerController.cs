using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Frontend.Models;
using ModularMonolith.Frontend.Services;
using System.Threading.Tasks;

namespace ModularMonolith.Frontend.Controllers
{
    // WebAPI/Controllers/CustomerController.cs
    using Microsoft.AspNetCore.Mvc;
    using CustomerManagement.Domain.Repositories;
    using CustomerManagement.Application.DTOs;
    using AutoMapper;
    using System.Threading.Tasks;

    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return View(customers);
        }

        // GET: Customer/Create
        public IActionResult Create() => View();

        // POST: Customer/Create
        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.CreateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerDto customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.UpdateCustomerAsync(id, customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
    }

}

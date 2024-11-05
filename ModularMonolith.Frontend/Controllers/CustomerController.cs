using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Frontend.Models;
using ModularMonolith.Frontend.Services;
using System.Threading.Tasks;

namespace ModularMonolith.Frontend.Controllers
{ 
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // List Action
        public async Task<IActionResult> List()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return View(customers);
        }

        // Details Action
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // Create Actions
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddCustomerAsync(model);
                return RedirectToAction("List");
            }
            return View(model);
        }

        // Edit Actions
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                await _customerService.UpdateCustomerAsync(id, model);
                return RedirectToAction("List");
            }
            return View(model);
        }

        // Delete Actions
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("List");
        }
    }
}

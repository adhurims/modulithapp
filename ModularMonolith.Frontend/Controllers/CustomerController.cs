using AutoMapper;
using CustomerManagement.Application.DTOs;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        //Test
        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllAsync();
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return View(customerDtos);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(customerDto);
                await _customerRepository.AddAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customerDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return View(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(customerDto);
                await _customerRepository.UpdateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customerDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return View(customerDto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            //if (customer != null) await _customerRepository.DeleteAsync(customer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return View(customerDto);
        }
    }

}

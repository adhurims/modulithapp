using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepository.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            await _customerRepository.UpdateAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            await _customerRepository.DeleteAsync(customer);
            return NoContent();
        }
    }
}
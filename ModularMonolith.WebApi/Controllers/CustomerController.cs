using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Identity;
using CustomerManagement.Domain.Repositories; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Frontend.Models;


namespace ModularMonolith.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomerController(ICustomerRepository customerRepository, UserManager<ApplicationUser> userManager)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.UserId))
            {
                // Generate UserId if not provided
                var user = new ApplicationUser
                {
                    UserName = customer.Email,
                    Email = customer.Email, 
                    PhoneNumber = customer.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, "DefaultPassword123!");
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                customer.UserId = user.Id;
            }

            await _customerRepository.AddAsync(customer);

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer customer)
        {
            if (id == 0)
                return BadRequest();
             
            if (customer.User == null && !string.IsNullOrEmpty(customer.UserId))
            {
                customer.User = await _userManager.FindByIdAsync(customer.UserId);
                if (customer.User == null)
                    return BadRequest("Associated user not found.");
            }

            await _customerRepository.UpdateAsync(customer);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
        
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
             
            await _customerRepository.DeleteAsync(customer);

            return NoContent();
        }
    }
}
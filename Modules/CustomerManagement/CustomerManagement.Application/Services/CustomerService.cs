using Microsoft.AspNetCore.Identity;
using CustomerManagement.Application.DTOs;
using CustomerManagement.Domain.Identity;
using CustomerManagement.Domain.Repositories;
using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerService(ICustomerRepository customerRepository, UserManager<ApplicationUser> userManager)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
        }

        public async Task CreateCustomerAsync(CustomerDto dto)
        { 
            var customer = new Customer(dto.Name, dto.Email, dto.Address, dto.PhoneNumber);
            await _customerRepository.AddAsync(customer);
             
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                CustomerId = customer.Id,  
                PhoneNumber = dto.PhoneNumber
            };
             
            var result = await _userManager.CreateAsync(user, "DefaultPassword123!"); // Replace with secure password logic
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
               
            var roleResult = await _userManager.AddToRoleAsync(user, "Business");
            if (!roleResult.Succeeded)
            {
                throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }
        }
    }
}

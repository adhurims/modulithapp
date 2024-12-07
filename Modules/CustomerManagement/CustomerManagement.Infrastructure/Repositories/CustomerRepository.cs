using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Repositories;
using CustomerManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerManagementDbContext _context;

        public CustomerRepository(CustomerManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task UpdateAsync(Customer customer)
        { 
            _context.Customers.Update(customer);
             
            if (customer.User != null)
            {
                _context.Users.Update(customer.User);
            }
             
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Customer customer)
        {
           
            var user = await _context.Users.FindAsync(customer.UserId);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
             
            _context.Customers.Remove(customer);
             
            await _context.SaveChangesAsync();
        }
    }
}

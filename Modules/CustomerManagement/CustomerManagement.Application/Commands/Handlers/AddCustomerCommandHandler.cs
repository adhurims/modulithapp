using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Commands.Handlers
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Name, request.Email, request.Address);
            await _customerRepository.AddAsync(customer);
            return true;
        }
    }
}

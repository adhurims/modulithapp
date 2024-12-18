﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Commands
{
    public class AddCustomerCommand : IRequest<bool>
    {
        public string Name { get; }
        public string Email { get; }
        public string Address { get; }
        public string PhoneNumber { get; }

        public AddCustomerCommand(string name, string email, string address, string phoneNumber)
        {
            Name = name;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}

﻿using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> PlaceOrderAsync(Order order);
    }
}

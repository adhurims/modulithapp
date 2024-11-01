using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs
{
    using System;
    using System.Collections.Generic;

    public class CreateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public List<CreateOrderItemDto> Items { get; set; }
    }
}

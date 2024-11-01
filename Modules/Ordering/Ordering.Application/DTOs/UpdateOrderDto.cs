using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs
{
    public class UpdateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public List<UpdateOrderItemDto> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Application.DTOs.Orders
{
    public class OrderDetailsDTO : OrderSummaryDTO
    {
        public string ShippingAddress { get; set; }
        public List<OrderItemDetailsDTO> OrderItems { get; set; }
    }
}

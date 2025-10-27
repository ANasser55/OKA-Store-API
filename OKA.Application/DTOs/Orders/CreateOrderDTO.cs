using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Application.DTOs.Orders
{
    public class CreateOrderDTO
    {
        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}

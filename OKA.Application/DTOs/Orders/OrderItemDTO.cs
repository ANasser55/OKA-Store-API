
using System.ComponentModel.DataAnnotations;


namespace OKA.Application.DTOs.Orders
{
    public class OrderItemDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

    }
}

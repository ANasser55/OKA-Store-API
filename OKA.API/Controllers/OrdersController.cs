using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OKA.Application.DTOs.Orders;
using OKA.Application.IService;

namespace OKA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int userId, [FromBody] CreateOrderDTO orderDTO)
        {
            var tempUserId = 1;

            var createdOrder = await _service.CreateOrder(tempUserId, orderDTO);

            if (createdOrder == null)
            {
                return BadRequest("Failed to create order. Check product stock.");
            }

            return CreatedAtAction(nameof(GetOrderDetails), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var tempUserId = 1;

            var orders = await _service.GetUserOrders(tempUserId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var order = await _service.GetOrderDetails(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}

using OKA.Application.DTOs.Orders;

namespace OKA.Application.IService
{
    public interface IOrdersService
    {
        Task<OrderDetailsDTO?> CreateOrder(int userId, CreateOrderDTO createOrderDTO);
        Task<OrderDetailsDTO?> GetOrderDetails(int orderId);
        Task<IEnumerable<OrderSummaryDTO>> GetUserOrders(int userId);
    }
}
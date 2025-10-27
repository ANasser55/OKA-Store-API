using OKA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Domain.IRepositories
{
    public interface IOrdersRepository
    {
        Task<Order> CreateOrder(Order order);
        Task<Order?> GetOrderDetails(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
    }
}

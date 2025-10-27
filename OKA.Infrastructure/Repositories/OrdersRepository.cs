using Microsoft.EntityFrameworkCore;
using OKA.Domain.Entities;
using OKA.Domain.IRepositories;
using OKA.Infrastructure.Data;


namespace OKA.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OKAStoreDbContext _context;

        public OrdersRepository(OKAStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public async Task<Order?> GetOrderDetails(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}

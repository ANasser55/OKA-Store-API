using OKA.Application.DTOs.Orders;
using OKA.Application.IService;
using OKA.Domain.Entities;
using OKA.Domain.IRepositories;


namespace OKA.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _orderRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersService(IOrdersRepository orderRepository, IProductsRepository productsRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDetailsDTO?> CreateOrder(int userId, CreateOrderDTO createOrderDTO)
        {
            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var item in createOrderDTO.OrderItems)
                {
                    var product = await _productsRepository.GetProductById(item.ProductId);

                    if (product == null || product.Quantity < item.Quantity)
                    {
                        return null;
                    }

                    var orderItem = new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        PriceAtPurchase = product.Price
                    };
                    orderItems.Add(orderItem);

                    var stockDecreased = await _productsRepository.TryDecreaseQuantity(item.ProductId, item.Quantity);

                    if (!stockDecreased)
                    {
                        throw new Exception($"Insufficient stock for product: {product.Name}");
                    }

                    totalAmount += item.Quantity * product.Price;


                }
                var order = new Order
                {
                    UserId = userId,
                    ShippingAddress = createOrderDTO.ShippingAddress,
                    OrderDate = DateTime.UtcNow,
                    Status = "Pending",
                    TotalAmount = totalAmount,
                    OrderItems = orderItems

                };

                var createdOrder = await _orderRepository.CreateOrder(order);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return await GetOrderDetails(createdOrder.Id);

            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return null;

            }
        }

        public async Task<OrderDetailsDTO?> GetOrderDetails(int orderId)
        {
            var order = await _orderRepository.GetOrderDetails(orderId);
            if (order == null) return null;

            return new OrderDetailsDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDetailsDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    PriceAtPurchase = oi.PriceAtPurchase
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderSummaryDTO>> GetUserOrders(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserId(userId);
            return orders.Select(o => new OrderSummaryDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status
            });
        }
    }
}

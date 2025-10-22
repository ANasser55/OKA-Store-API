using OKA.Domain.Entities;
using OKA.Domain.ValueObjects;

namespace OKA.Domain.IRepositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProducts(ProductsFilterParams filterParams);
        Task<int> GetTotalCount(string? searchTerm, int? categoryId);
        Task<Product?> GetProductById(int id);
    }
}
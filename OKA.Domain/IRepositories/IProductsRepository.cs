using OKA.Domain.Entities;

namespace OKA.Domain.IRepositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProducts(string? searchTerm, int page, int pageSize);
        Task<int> GetTotalCount(string? searchTerm);
    }
}
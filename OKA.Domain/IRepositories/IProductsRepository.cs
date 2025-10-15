using OKA.Domain.Entities;

namespace OKA.Domain.IRepositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
using OKA.Application.DTOs;
using OKA.Domain.Entities;

namespace OKA.Application.IService
{
    public interface IProductsService
    {
        Task<PageDTO<Product>> GetAllProducts(string? searchTerm, int page, int pageSize);
    }
}
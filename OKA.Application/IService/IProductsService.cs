using OKA.Application.DTOs;
using OKA.Domain.Entities;

namespace OKA.Application.IService
{
    public interface IProductsService
    {
        Task<PageDTO<ProductsDTO>> GetAllProducts(string? searchTerm, string? sortColumn, string? sortBy, int page, int pageSize);
        Task<ProductsDTO?> GetProductById(int id);
    }
}
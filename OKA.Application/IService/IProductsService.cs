using OKA.Application.DTOs;
using OKA.Domain.ValueObjects;

namespace OKA.Application.IService
{
    public interface IProductsService
    {
        Task<PageDTO<ProductsDTO>> GetAllProducts(ProductsFilterParams filterParams);
        Task<ProductsDTO?> GetProductById(int id);
    }
}
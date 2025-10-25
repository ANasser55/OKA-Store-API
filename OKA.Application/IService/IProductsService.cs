using OKA.Application.DTOs;
using OKA.Application.DTOs.Products;
using OKA.Application.Enums;
using OKA.Domain.Entities;
using OKA.Domain.ValueObjects;

namespace OKA.Application.IService
{
    public interface IProductsService
    {
        Task<PageDTO<ProductDetailsDTO>> GetAllProducts(ProductsFilterParams filterParams);
        Task<ProductDetailsDTO?> GetProductById(int id);
        Task<int> CreateProduct(CreateOrUpdateProductDTO product);
        Task<ProductUpdateResult> UpdateProduct(int id, CreateOrUpdateProductDTO productDTO);
        Task<bool> DeleteProduct(int id);
    }
}
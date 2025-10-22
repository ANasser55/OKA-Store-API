using OKA.Application.DTOs;
using OKA.Application.IService;
using OKA.Domain.IRepositories;
using OKA.Domain.ValueObjects;


namespace OKA.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _repository;

        public ProductsService(IProductsRepository repository)
        {
            this._repository = repository;
        }

        public async Task<PageDTO<ProductsDTO>> GetAllProducts(ProductsFilterParams filterParams)
        {
            var products = await _repository.GetAllProducts(filterParams);
            var totalPorductsCount = await _repository.GetTotalCount(filterParams.SearchTerm, filterParams.CategoryId);
            var productsDTO = products.Select(p => new ProductsDTO()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Quantity = p.Quantity,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category?.Name ?? "Uncategorized"
            });

            return new PageDTO<ProductsDTO>(productsDTO, filterParams.Page, filterParams.PageSize, totalPorductsCount);
        }
        public async Task<ProductsDTO?> GetProductById(int id)
        {
            var product = await _repository.GetProductById(id);
            if (product == null)
                return null;

            return new ProductsDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                CategoryName = product.Category?.Name ?? "Uncategorized"
            };


        }
    }
}

using OKA.Application.DTOs;
using OKA.Application.IService;
using OKA.Domain.IRepositories;


namespace OKA.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _repository;

        public ProductsService(IProductsRepository repository)
        {
            this._repository = repository;
        }

        public async Task<PageDTO<ProductsDTO>> GetAllProducts(string? searchTerm, string? sortColumn, string? sortBy, int page, int pageSize)
        {
            var products = await _repository.GetAllProducts(searchTerm, sortColumn, sortBy, page, pageSize);
            var totalPorductsCount = await _repository.GetTotalCount(searchTerm);
            var productsDTO = products.Select(p => new ProductsDTO()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Quantity = p.Quantity,
                ImageUrl = p.ImageUrl,
                //CategoryName = p.Category.Name
            });

            return new PageDTO<ProductsDTO>(productsDTO, page, pageSize, totalPorductsCount);
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
                //CategoryName = product.Category.Name
            };


        }
    }
}

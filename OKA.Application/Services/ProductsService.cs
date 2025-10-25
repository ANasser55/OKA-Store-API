using OKA.Application.DTOs;
using OKA.Application.DTOs.Products;
using OKA.Application.Enums;
using OKA.Application.IService;
using OKA.Domain.Entities;
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


        public async Task<PageDTO<ProductDetailsDTO>> GetAllProducts(ProductsFilterParams filterParams)
        {
            var products = await _repository.GetAllProducts(filterParams);
            var totalPorductsCount = await _repository.GetTotalCount(filterParams.SearchTerm, filterParams.CategoryId);
            var productsDTO = products.Select(p => new ProductDetailsDTO()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Quantity = p.Quantity,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category?.Name ?? "Uncategorized"
            });

            return new PageDTO<ProductDetailsDTO>(productsDTO, filterParams.Page, filterParams.PageSize, totalPorductsCount);
        }
        public async Task<ProductDetailsDTO?> GetProductById(int id)
        {
            var product = await _repository.GetProductById(id);
            if (product == null)
                return null;

            return new ProductDetailsDTO()
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
        public async Task<int> CreateProduct(CreateOrUpdateProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                Description = productDTO.Description,
                Quantity = productDTO.Quantity,
                ImageUrl = productDTO.ImageUrl,
                CategoryId = productDTO.CategoryId
            };

            await _repository.CreateProduct(product);

            return product.Id;
        }

        public async Task<ProductUpdateResult> UpdateProduct(int id, CreateOrUpdateProductDTO productDTO)
        {
            var currentProduct = await _repository.GetProductById(id);
            if (currentProduct == null)
                return ProductUpdateResult.NotFound;

            currentProduct.Name = productDTO.Name;
            currentProduct.Description = productDTO.Description;
            currentProduct.CategoryId = productDTO.CategoryId;
            currentProduct.Price = productDTO.Price;
            currentProduct.ImageUrl = productDTO.ImageUrl;
            currentProduct.Quantity = productDTO.Quantity;

            if (await _repository.UpdateProduct())
            {
                return ProductUpdateResult.Success;
            }
            else
                return ProductUpdateResult.Failed;

        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _repository.GetProductById(id);
            if (product == null)
                return false;

            return await _repository.DeleteProduct(product);
        }
    }
}

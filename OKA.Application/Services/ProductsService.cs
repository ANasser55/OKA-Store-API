using OKA.Application.DTOs;
using OKA.Application.IService;
using OKA.Domain.Entities;
using OKA.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _repository;

        public ProductsService(IProductsRepository repository)
        {
            this._repository = repository;
        }

        public async Task<PageDTO<Product>> GetAllProducts(string? searchTerm, int page, int pageSize)
        {
            var products = await _repository.GetAllProducts(searchTerm, page, pageSize);
            var totalPorductsCount = await _repository.GetTotalCount(searchTerm);

            return new PageDTO<Product>(products, page, pageSize, totalPorductsCount);
        }
    }
}

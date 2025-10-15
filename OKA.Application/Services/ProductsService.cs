using OKA.Domain.Entities;
using OKA.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Application.Services
{
    public class ProductsService
    {
        private readonly IProductsRepository _repository;

        ProductsService(IProductsRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _repository.GetAllProducts();
        }
    }
}

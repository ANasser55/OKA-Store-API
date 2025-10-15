using OKA.Domain.Entities;
using OKA.Domain.IRepositories;
using OKA.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly OKAStoreDbContext _context;

        ProductsRepository(OKAStoreDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = _context.Products;
            return products;
        }
    }
}

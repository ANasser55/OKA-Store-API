using OKA.Application.DTOs;
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

        public ProductsRepository(OKAStoreDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts(string? searchTerm, int page, int pageSize)
        {
            if (searchTerm == null)
            {
                return _context.Products.Skip((page - 1) * pageSize).Take(pageSize);
            }

            return _context.Products.Where(p => p.Name.Contains(searchTerm)).Skip((page - 1) * pageSize).Take(pageSize);

        }
        public async Task<int> GetTotalCount(string? searchTerm)
        {
            if (searchTerm == null)
                return _context.Products.Count();
            return _context.Products.Where(p => p.Name.Contains(searchTerm)).Count();
        }
    }
}

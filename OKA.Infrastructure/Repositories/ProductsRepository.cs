using Microsoft.EntityFrameworkCore;
using OKA.Domain.Entities;
using OKA.Domain.IRepositories;
using OKA.Infrastructure.Data;


namespace OKA.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly OKAStoreDbContext _context;

        public ProductsRepository(OKAStoreDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts(string? searchTerm, string? sortColumn, string? sortBy, int page, int pageSize)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.Name.Contains(searchTerm));



            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            if (sortBy?.ToLower() == "desc")
            {
                switch (sortColumn?.ToLower())
                {
                    case "price":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.Name);
                        break;
                }
            }
            else
            {
                switch (sortColumn?.ToLower())
                {
                    case "price":
                        query = query.OrderBy(p => p.Price);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }


            return await query.ToListAsync();

        }
        public async Task<int> GetTotalCount(string? searchTerm)
        {
            if (searchTerm == null)
                return await _context.Products.CountAsync();
            return await _context.Products.Where(p => p.Name.Contains(searchTerm)).CountAsync();
        }
        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }


    }
}

using Microsoft.EntityFrameworkCore;
using OKA.Application.DTOs.Products;
using OKA.Domain.Entities;
using OKA.Domain.IRepositories;
using OKA.Domain.ValueObjects;
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

        public async Task<IEnumerable<Product>> GetAllProducts(ProductsFilterParams filterParams)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            query = ApplyFilters(query, filterParams.SearchTerm, filterParams.CategoryId);



            if (filterParams.SortBy?.ToLower() == "desc")
            {
                switch (filterParams.SortColumn?.ToLower())
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
                switch (filterParams.SortColumn?.ToLower())
                {
                    case "price":
                        query = query.OrderBy(p => p.Price);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }

            query = query.Skip((filterParams.Page - 1) * filterParams.PageSize).Take(filterParams.PageSize);



            return await query.ToListAsync();

        }
        public async Task<int> GetTotalCount(string? searchTerm, int? categoryId)
        {
            var query = _context.Products.AsQueryable();

            query = ApplyFilters(query, searchTerm, categoryId);

            return await query.CountAsync();
        }
        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<int> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> UpdateProduct()
        {
            try
            {
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;

        }

        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, string? searchTerm, int? categoryId)
        {
            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.Name.Contains(searchTerm));
            if (categoryId != null)
                query = query.Where(p => p.CategoryId == categoryId);

            return query;
        }

        public async Task<bool> TryDecreaseQuantity(int productId, int quantity)
        {
            var affectedRows = await _context.Database.ExecuteSqlInterpolatedAsync($"""
                update products 
                set Quantity = Quantity - {quantity}
                where id = {productId} and quantity >= {quantity}
                """);

            return affectedRows > 0;
        }
    }
}

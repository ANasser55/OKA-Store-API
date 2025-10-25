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

            if (!string.IsNullOrEmpty(filterParams.SearchTerm) && !string.IsNullOrWhiteSpace(filterParams.SearchTerm))
                query = query.Where(p => p.Name.Contains(filterParams.SearchTerm));

            if (filterParams.CategoryId != null)
                query = query.Where(p => p.CategoryId == filterParams.CategoryId);



            query = query.Skip((filterParams.Page - 1) * filterParams.PageSize).Take(filterParams.PageSize);

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


            return await query.ToListAsync();

        }
        public async Task<int> GetTotalCount(string? searchTerm, int? categoryId)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.Name.Contains(searchTerm));
            if (categoryId != null)
                query = query.Where(p => p.CategoryId == categoryId);
            return await query.CountAsync();
        }
        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<int> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            //await _context.SaveChangesAsync();
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



    }
}

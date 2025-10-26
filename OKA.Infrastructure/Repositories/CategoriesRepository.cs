using Microsoft.EntityFrameworkCore;
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
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly OKAStoreDbContext _context;

        public CategoriesRepository(OKAStoreDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateCategory()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> IsCategoryUsed(int id)
        {
            return await _context.Products.AnyAsync(p => p.CategoryId == id);
        }
    }
}

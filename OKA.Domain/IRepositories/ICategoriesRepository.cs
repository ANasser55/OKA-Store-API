using OKA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Domain.IRepositories
{
    public interface ICategoriesRepository
    {
        Task<Category> CreateCategory(Category category);
        Task<bool> DeleteCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<bool> IsCategoryUsed(int id);
        Task<bool> UpdateCategory();
    }
}

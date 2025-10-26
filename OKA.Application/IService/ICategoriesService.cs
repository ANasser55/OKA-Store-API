using OKA.Application.DTOs.Categories;
using OKA.Application.Enums;

namespace OKA.Application.IService
{
    public interface ICategoriesService
    {
        Task<CategoryDTO> CreateCategory(CreateOrUpdateCategoryDTO categoryDTO);
        Task<RequestResult> DeleteCategory(int id);
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO?> GetCategoryById(int id);
        Task<RequestResult> UpdateCategory(int id, CreateOrUpdateCategoryDTO categoryDTO);
    }
}
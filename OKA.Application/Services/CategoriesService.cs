using OKA.Application.DTOs.Categories;
using OKA.Application.Enums;
using OKA.Application.IService;
using OKA.Domain.Entities;
using OKA.Domain.IRepositories;


namespace OKA.Application.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _repository;

        public CategoriesService(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await _repository.GetAllCategories();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
            });
        }

        public async Task<CategoryDTO?> GetCategoryById(int id)
        {
            var category = await _repository.GetCategoryById(id);
            if (category == null)
                return null;

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoryDTO> CreateCategory(CreateOrUpdateCategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
            };

            var createdCategory = await _repository.CreateCategory(category);

            return new CategoryDTO
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
            };
        }

        public async Task<RequestResult> UpdateCategory(int id, CreateOrUpdateCategoryDTO categoryDTO)
        {
            var currentCategory = await _repository.GetCategoryById(id);
            if (currentCategory == null)
                return RequestResult.NotFound;

            currentCategory.Name = categoryDTO.Name;

            if (await _repository.UpdateCategory())
            {
                return RequestResult.Success;
            }
            return RequestResult.Failed;
        }

        public async Task<RequestResult> DeleteCategory(int id)
        {
            var category = await _repository.GetCategoryById(id);
            if (category == null)
                return RequestResult.NotFound;

            if (await _repository.IsCategoryUsed(id))
                return RequestResult.Failed;

            await _repository.DeleteCategory(category);
            return RequestResult.Success;
        }
    }
}

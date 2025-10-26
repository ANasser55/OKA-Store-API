using Microsoft.AspNetCore.Mvc;
using OKA.Application.DTOs.Categories;
using OKA.Application.Enums;
using OKA.Application.IService;

namespace OKA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _service;

        public CategoriesController(ICategoriesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _service.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _service.GetCategoryById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateOrUpdateCategoryDTO categoryDTO)
        {
            var newCategory = await _service.CreateCategory(categoryDTO);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateOrUpdateCategoryDTO categoryDTO)
        {
            var result = await _service.UpdateCategory(id, categoryDTO);
            if (result == RequestResult.NotFound)
                return NotFound();
            else if (result == RequestResult.Failed)
                return BadRequest("Nothing changed");

            return NoContent();
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _service.DeleteCategory(id);
            if (result == RequestResult.NotFound)
                return NotFound();
            else if (result == RequestResult.Failed)
            {
                return BadRequest("Category is currently in use by products");
            }

            return NoContent();
        }
    }
}

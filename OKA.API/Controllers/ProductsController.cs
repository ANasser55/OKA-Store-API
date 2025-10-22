using Microsoft.AspNetCore.Mvc;
using OKA.Application.IService;
using OKA.Domain.ValueObjects;

namespace OKA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            this._service = service;
        }

        [HttpGet()]
        //public async Task<IActionResult> GetAllProducts(string? searchTerm, string? sortColumn, string? sortBy, int page = 1, int pageSize = 10)
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductsFilterParams filterParams)
        {
            //var products = await _service.GetAllProducts(searchTerm, sortColumn, sortBy, page, pageSize);
            //var products = await _service.GetAllProducts(filterParams.SearchTerm, filterParams.SortColumn, filterParams.SortBy, filterParams.Page, filterParams.PageSize);
            var products = await _service.GetAllProducts(filterParams);

            return Ok(products);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        { 
            var product = await _service.GetProductById(id);
            return Ok(product);
        }
    }
}

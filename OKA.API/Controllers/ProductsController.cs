using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OKA.Application.IService;

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

        [HttpGet]
        public async Task<IActionResult> Products(string? searchTerm, int page = 1, int pageSize = 10)
        {
            var products = await _service.GetAllProducts(searchTerm, page, pageSize);

            return Ok(products);
        }
    }
}

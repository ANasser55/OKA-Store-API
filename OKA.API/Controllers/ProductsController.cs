using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OKA.Application.Services;

namespace OKA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _service;

        ProductsController(ProductsService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            await _service.GetAllProducts();
            return Ok();
        }
    }
}

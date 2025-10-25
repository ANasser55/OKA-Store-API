using Microsoft.AspNetCore.Mvc;
using OKA.Application.DTOs.Products;
using OKA.Application.Enums;
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
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductsFilterParams filterParams)
        {

            var products = await _service.GetAllProducts(filterParams);

            return Ok(products);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductById(id);
            return Ok(product);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateProduct([FromBody] CreateOrUpdateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productId = await _service.CreateProduct(productDTO);

            return Ok(productId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateOrUpdateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.UpdateProduct(id, productDTO);
            if (result == ProductUpdateResult.Failed)
                return BadRequest();
            else if (result == ProductUpdateResult.NotFound)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _service.DeleteProduct(id))
                return NoContent();
            return NotFound();
        }
    }
}
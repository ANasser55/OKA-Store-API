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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateProduct([FromBody] CreateOrUpdateProductDTO productDTO)
        {

            var productId = await _service.CreateProduct(productDTO);

            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateOrUpdateProductDTO productDTO)
        {

            var result = await _service.UpdateProduct(id, productDTO);
            if (result == RequestResult.Failed)
                return BadRequest();
            else if (result == RequestResult.NotFound)
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
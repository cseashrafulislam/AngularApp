using AngularApp1.Server.Entities;
using AngularApp1.Server.Services;
using AngularApp1.Server.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        // Injecting ProductService into the controller
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // Get a product by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound(); // Return 404 if product is not found
            }
            return Ok(product); // Return 200 with the product
        }

        // Get all products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productService.GetList();
            return Ok(products); // Return 200 with the list of products
        }

        // Update a product
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if the model is invalid
            }

            var result = await _productService.UpdateProduct(model);
            if (result > 0)
            {
                return NoContent(); // Return 204 if the update was successful
            }
            return NotFound(); // Return 404 if the product to update is not found
        }

        // Add a new product
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if the model is invalid
            }

            var product = await _productService.AddProduct(model);
            if (product != null)
            {
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product); // Return 201 Created
            }
            return BadRequest("Product creation failed."); // Return 400 if creation failed
        }

        // Delete a product by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result > 0)
            {
                return NoContent(); // Return 204 if deletion was successful
            }
            return NotFound(); // Return 404 if product to delete is not found
        }
    }
}

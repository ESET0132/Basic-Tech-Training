using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repositories;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product with ID {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdProduct = await _productRepository.CreateAsync(product);
                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest("ID in URL does not match ID in body");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedProduct = await _productRepository.UpdateAsync(id, product);

                if (updatedProduct == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productRepository.DeleteAsync(id);

                if (!result)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
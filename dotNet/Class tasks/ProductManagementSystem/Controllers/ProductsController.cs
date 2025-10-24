using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Models.DTOs;
using ProductManagementSystem.Models.Entities;
using ProductManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                StockQuantity = createProductDto.StockQuantity,
                Description = createProductDto.Description,
                Category = createProductDto.Category,
                Specifications = createProductDto.Specifications != null ?
                    System.Text.Json.JsonSerializer.Serialize(createProductDto.Specifications) : "{}"
            };

            var createdProduct = await _productRepository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }

            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;
            existingProduct.StockQuantity = productDto.StockQuantity;
            existingProduct.Description = productDto.Description;
            existingProduct.Category = productDto.Category;

            if (!string.IsNullOrEmpty(productDto.Specifications))
            {
                existingProduct.Specifications = productDto.Specifications;
            }

            await _productRepository.UpdateProductAsync(existingProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("category/{category}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(category);
            return Ok(products);
        }

        [HttpGet("sales-summary")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<object>> GetProductSalesSummary()
        {
            var summary = await _productRepository.GetProductSalesSummaryAsync();
            return Ok(summary);
        }

        [HttpPut("{id}/specifications")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<string>> UpdateProductSpecifications(int id, Dictionary<string, object> specifications)
        {
            try
            {
                var result = await _productRepository.UpdateProductSpecificationsAsync(id, specifications);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating specifications: {ex.Message}");
            }
        }

        [HttpGet("{id}/specifications")]
        [AllowAnonymous]
        public async Task<ActionResult<Dictionary<string, object>>> GetProductSpecifications(int id)
        {
            var specifications = await _productRepository.GetProductSpecificationsAsync(id);
            return Ok(specifications);
        }
    }
}
using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productDto);
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            _mapper.Map(productDto, product);
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            await _productRepository.DeleteAsync(product);
            return NoContent();
        }
    }
}


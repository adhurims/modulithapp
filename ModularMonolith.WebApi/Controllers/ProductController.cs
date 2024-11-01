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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateProduct(ProductDto productDto)
        //{
        //    var product = _mapper.Map<Product>(productDto);
        //    await _productRepository.AddAsync(product);
        //    return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productDto);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            product.UpdatePrice(productDto.Price);
            product.UpdateStock(productDto.StockLevel);
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

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

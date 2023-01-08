using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public ActionResult<ProductDto> AddProduct(ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                Guid productId = _productService.AddProduct(product);
                return CreatedAtAction("AddProduct", new { Id = productId }, product);
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
            catch (DuplicateNameException e)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDtoId>> GetAllProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            return _productService.DeleteProduct(id) ? (IActionResult)NoContent() : NotFound();
        }

        [HttpPut]
        public IActionResult StockProduct(Guid id, ProductDtoId productDtoId, int quantity)
        {
            try 
            {
                if (id != productDtoId.ProductId)
                {
                    return BadRequest("ProductId mismatch!");
                }

                var productToUpdate = _productService.GetProduct(id);

                if (productToUpdate == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                _productService.StockProduct(productDtoId, quantity);
                return Accepted();
            } 
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPut]
        public IActionResult SellProduct(IEnumerable<ProductDtoId> products)
        {
            try
            {
                foreach (var product in products)
                {
                    var productToUpdate = _productService.GetProduct(product.ProductId);

                    if (productToUpdate == null)
                    {
                        return NotFound($"Product with Id = {product.ProductId} not found");
                    }
                }

                _productService.SellProduct(products);
                return Accepted();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}

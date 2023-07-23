using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using System.Data;
using System.Net;

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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddProduct(ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (product.BrandId.Equals(new Guid()) || product.CategoryId.Equals(new Guid()))
                {
                    return BadRequest("Missing parameters!");      //TODO
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
               return BadRequest();    //TODO
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public ActionResult<IEnumerable<StockedProductDtoId>> GetAllStockedProducts()   //TODO
        {
            //return Ok(_productService.GetStockedProducts());
            return Ok(_productService.GetProducts());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<StockedProductDtoId>> GetAllProducts()  //TODO
        {
            return Ok(_productService.GetProducts());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            return _productService.DeleteProduct(id) ? (IActionResult)NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult StockProduct(Guid id, ProductDtoId productDtoId, int quantity)    //TODO
        {
            try 
            {
                if (id != productDtoId.ProductId)
                {
                    return BadRequest("ProductId mismatch!");    //TODO
                }

                var productToUpdate = _productService.GetProduct(id);

                if (productToUpdate == null)
                {
                    return NotFound($"Product with Id = {id} not found");    //TODO
                }

                _productService.StockProduct(productDtoId, quantity);
                return Accepted();
            } 
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpPut]
        public IActionResult SellProduct(IEnumerable<ProductDtoId> products)    //TODO
        {
            try
            {
                foreach (var product in products)
                {
                    var productToUpdate = _productService.GetProduct(product.ProductId);

                    if (productToUpdate == null)
                    {
                        return NotFound($"Product with Id = {product.ProductId} not found");    //TODO
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

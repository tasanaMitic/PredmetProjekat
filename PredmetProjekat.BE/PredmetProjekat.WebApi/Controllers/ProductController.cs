using Microsoft.AspNetCore.Authentication;
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
        public ActionResult AddProduct([FromBody] ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (product.BrandId.Equals(new Guid()) || product.CategoryId.Equals(new Guid()))
                {
                    return Problem($"Something went wrong in the {nameof(AddProduct)}!", "Brand/Category id missing!", statusCode: 404);
                }

                Guid productId = _productService.AddProduct(product);
                return CreatedAtAction("AddProduct", new { Id = productId }, product);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddProduct)}!", ex.Message, statusCode: 404);
            }
            catch (ArgumentException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddProduct)}!", ex.Message, statusCode: 400);
            }
            catch (DuplicateNameException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddProduct)}!", ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(AddProduct)}!", ex.Message, statusCode: 500);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        [Route("stocked")]
        public ActionResult<IEnumerable<StockedProductDtoId>> GetAllStockedProducts()
        {
            try
            {
                return Ok(_productService.GetStockedProducts());
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(GetAllStockedProducts)}!", ex.Message, statusCode: 500);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<StockedProductDtoId>> GetAllProducts()
        {
            try
            {
                return Ok(_productService.GetProducts());
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(GetAllProducts)}!", ex.Message, statusCode: 500);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            try
            {
                return _productService.DeleteProduct(id) ? NoContent() : Problem($"Something went wrong in the {nameof(DeleteProduct)}!", "Product with that id not found!", statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteProduct)}!", ex.Message, statusCode: 500);
            }           
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public IActionResult StockProduct(Guid id, Quantity quantity)
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _productService.StockProduct(id, quantity.Value);
                return Accepted();
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(StockProduct)}!", ex.Message, statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(StockProduct)}!", ex.Message, statusCode: 500);
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpPatch("{id}")]
        public IActionResult SellProduct(IEnumerable<ProductDtoId> products)    //TODO
        {
            try
            {
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

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Dtos.ProductDtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IService;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthManager _authManager;
        public ProductController(IProductService productService, IAuthManager authManager)
        {
            _productService = productService;
            _authManager = authManager;
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
        public ActionResult<IEnumerable<StockedProductDto>> GetAllStockedProducts()
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
        public ActionResult<IEnumerable<StockedProductDto>> GetAllProducts()
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
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteProduct)}!", "Product with that id not found!", statusCode: 404);
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
                return NoContent();
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
        [HttpPatch]
        public IActionResult SellProduct(SaleDto saleDto)
        {
            try
            {
                var tokenString = HttpContext.Request.Headers["Authorization"].ToString();
                var username = _authManager.DecodeToken(tokenString);

                _productService.SellProduct(saleDto, username);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(SellProduct)}!", ex.Message, statusCode: 500);
            }
        }
    }
}

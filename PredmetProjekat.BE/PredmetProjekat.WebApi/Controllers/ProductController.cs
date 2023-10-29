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

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        [Route("stocked")]
        public ActionResult<IEnumerable<StockedProductDto>> GetAllStockedProducts()
        {
            return Ok(_productService.GetStockedProducts());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<StockedProductDto>> GetAllProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<StockedProductDto>> DeleteProduct(Guid id)
        {
            return Ok(_productService.DeleteProduct(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public IActionResult StockProduct([FromRoute] Guid id, [FromBody] Quantity quantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productService.StockProduct(id, quantity.Value);
            return NoContent();
        }

        [Authorize(Roles = "Employee")]
        [HttpPatch]
        public IActionResult SellProduct([FromBody] SaleDto saleDto)
        {
            var tokenString = HttpContext.Request.Headers["Authorization"].ToString();
            var username = _authManager.DecodeToken(tokenString);

            _productService.SellProduct(saleDto, username);
            return NoContent();
        }
    }
}

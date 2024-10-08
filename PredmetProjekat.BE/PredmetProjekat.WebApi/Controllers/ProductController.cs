﻿using Microsoft.AspNetCore.Authentication;
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
        private readonly ISaleService _saleService;
        private readonly IAuthManager _authManager;
        public ProductController(IProductService productService, ISaleService saleService, IAuthManager authManager)
        {
            _productService = productService;
            _saleService = saleService;
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

            string productId = _productService.AddProduct(product);
            return CreatedAtAction("AddProduct", new { Id = productId }, product);
        }

        [Authorize(Roles = "Employee")]
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
        [HttpDelete("{productId}")]
        public ActionResult<IEnumerable<StockedProductDto>> DeleteProduct(string productId)
        {
            return Ok(_productService.DeleteProduct(productId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        [Route("stock/{productId}")]
        public ActionResult<IEnumerable<StockedProductDto>> StockProduct([FromRoute] string productId, [FromBody] QuantityDto quantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_productService.StockProduct(productId, quantity.Value));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        [Route("price/{productId}")]
        public ActionResult<IEnumerable<StockedProductDto>> UpdateProduct([FromRoute] string productId, [FromBody] PriceDto price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_productService.UpdateProductPrice(productId, price.Value));
        }

        [Authorize(Roles = "Employee")]
        [HttpPatch]
        public IActionResult SellProduct([FromBody] SaleDto saleDto)
        {
            var tokenString = HttpContext.Request.Headers["Authorization"].ToString();
            var username = _authManager.DecodeToken(tokenString);

            _saleService.SellProduct(saleDto, username);
            return NoContent();
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        [Route("filter")]
        public ActionResult<FilterSearchDto> GetFilteredSales([FromQuery] FilterParams filterParams)
        {
            var tokenString = HttpContext.Request.Headers["Authorization"].ToString();
            var username = _authManager.DecodeToken(tokenString); 
            return Ok(_saleService.GetFilteredSales(filterParams, username));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        [Route("pdf")]
        public ActionResult CreatePdf([FromQuery] FilterParams filterParams)
        {
            var tokenString = HttpContext.Request.Headers["Authorization"].ToString();
            var username = _authManager.DecodeToken(tokenString);

            _saleService.CreatePDF(filterParams, username);
            return NoContent();
        }
    }
}

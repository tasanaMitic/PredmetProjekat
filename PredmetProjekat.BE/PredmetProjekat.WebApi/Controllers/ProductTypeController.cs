using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos.ProductDtos;
using PredmetProjekat.Common.Interfaces.IService;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService; 
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddProductType([FromBody] ProductTypeDto productTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productTypeDto.Attributes.Count() == 0)
            {
                return Problem($"Something went wrong in the {nameof(AddProductType)}!", "Attributes missing!", statusCode: 404);
            }

            var productType = _productTypeService.AddProductType(productTypeDto);
            return CreatedAtAction("AddProductType", new { Id = productType.ProductTypeId }, productType);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDtoId>> GetAllProductTypes()
        {
            return Ok(_productTypeService.GetProductTypes());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<ProductTypeDtoId>> DeleteProductType([FromRoute] Guid id)
        {
            return Ok(_productTypeService.DeleteProductTypes(id));
        }
    }
}

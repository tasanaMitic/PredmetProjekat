using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces.IService;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<BrandDto> AddBrand([FromBody]BrandDto brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid brandId = _brandService.AddBrand(brand);
            return CreatedAtAction("AddBrand", new { Id = brandId }, brand);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<BrandDtoId>> GetAllBrands()
        {
            return Ok(_brandService.GetBrands());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(Guid id)
        {
            _brandService.DeleteBrand(id);
            return NoContent();
        }
    }
}

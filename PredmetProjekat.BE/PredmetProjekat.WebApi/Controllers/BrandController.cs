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
        public ActionResult<BrandDto> AddBrand([FromBody]BrandDto brandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brand = _brandService.AddBrand(brandDto);
            return CreatedAtAction("AddBrand", new { Id = brand.BrandId }, brand);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<BrandDtoId>> GetAllBrands()
        {
            return Ok(_brandService.GetBrands());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<BrandDtoId>> DeleteBrand([FromRoute] Guid id)
        {
            //todo logicko brisanje
            return Ok(_brandService.DeleteBrand(id));
        }
    }
}

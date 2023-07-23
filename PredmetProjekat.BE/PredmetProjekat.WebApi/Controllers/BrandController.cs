using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
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

        [Authorize]
        [HttpPost]
        public ActionResult<BrandDto> AddBrand(BrandDto brand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Guid brandId = _brandService.AddBrand(brand);
                return CreatedAtAction("AddBrand", new { Id = brandId }, brand);
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
            catch (DuplicateNameException e)
            {
                return BadRequest("Duplicate name!");   //TODO
            }
            catch (Exception e)
            {
                return BadRequest(e); ;   //TODO
            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<BrandDtoId>> GetAllBrands()
        {
            return Ok(_brandService.GetBrands());
            
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(Guid id)
        {
            return _brandService.DeleteBrand(id) ? (IActionResult)NoContent() : NotFound();
        }
    }
}

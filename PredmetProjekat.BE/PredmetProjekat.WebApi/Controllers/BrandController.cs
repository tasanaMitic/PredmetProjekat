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

        [Authorize(Roles = "Admin")]
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
            catch (ArgumentException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddBrand)}!", ex.Message, statusCode: 400);
            }
            catch (DuplicateNameException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddBrand)}!", ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(AddBrand)}!", ex.Message, statusCode: 500);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<BrandDtoId>> GetAllBrands()
        {
            try
            {
                return Ok(_brandService.GetBrands());
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(GetAllBrands)}!", ex.Message, statusCode: 500);
            }            
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(Guid id)
        {
            try
            {
                return _brandService.DeleteBrand(id) ? NoContent() : Problem($"Something went wrong in the {nameof(DeleteBrand)}!", "Brand with that id not found!", statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteBrand)}!", ex.Message, statusCode: 500);
            }
        }
    }
}

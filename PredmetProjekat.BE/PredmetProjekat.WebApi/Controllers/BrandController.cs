using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        [HttpPost]
        public ActionResult<BrandDto> AddBrand(BrandDto brand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                //Guid filmId = _filmService.AddFilm(film);
                return CreatedAtAction("AddFilm", new { Id = "" }, brand);
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
            catch (DuplicateNameException e)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<BrandDtoId>> GetAllBrands()
        {
            //return Ok(_filmService.GetAllFilms());
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(Guid id)
        {
            //return _filmService.DeleteFilm(id) ? (IActionResult)NoContent() : NotFound();
            return Ok();
        }
    }
}

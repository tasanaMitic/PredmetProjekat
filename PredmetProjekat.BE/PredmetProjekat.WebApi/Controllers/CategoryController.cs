using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        public ActionResult<CategoryDto> AddCategory(CategoryDto category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                //Guid filmId = _filmService.AddFilm(film);
                return CreatedAtAction("AddCategory", new { Id = "" }, category);
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
        public ActionResult<IEnumerable<CategoryDtoId>> GetAllCategories()
        {
            //return Ok(_filmService.GetAllFilms());
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            //return _filmService.DeleteFilm(id) ? (IActionResult)NoContent() : NotFound();
            return Ok();
        }
    }
}

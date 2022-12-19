using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ProductDto> AddProduct(ProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                //Guid filmId = _filmService.AddFilm(film);
                return CreatedAtAction("AddProduct", new { Id = "" }, product);
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
        public ActionResult<IEnumerable<ProductDtoId>> GetAllProducts()
        {
            //return Ok(_filmService.GetAllFilms());
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            //return _filmService.DeleteFilm(id) ? (IActionResult)NoContent() : NotFound();
            return Ok();
        }
    }
}

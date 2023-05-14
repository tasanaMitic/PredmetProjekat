using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService; 
        }
        [HttpPost]
        public ActionResult<CategoryDto> AddCategory(CategoryDto category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Guid categoryId = _categoryService.AddCategory(category);
                return CreatedAtAction("AddCategory", new { Id = categoryId }, category);
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
            catch (DuplicateNameException e)
            {
                return BadRequest("Duplicate name!");   //TODO fix this
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDtoId>> GetAllCategories()
        {
            return Ok(_categoryService.GetCategories());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            return _categoryService.DeleteCategory(id) ? (IActionResult)NoContent() : NotFound();
        }
    }
}

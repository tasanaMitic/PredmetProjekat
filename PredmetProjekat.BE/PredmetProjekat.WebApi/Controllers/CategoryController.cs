using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces.IService;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<CategoryDto> AddCategory([FromBody]CategoryDto category)
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
            catch (ArgumentException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddCategory)}!", ex.Message, statusCode: 400);
            }
            catch (DuplicateNameException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddCategory)}!", ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(AddCategory)}!", ex.Message, statusCode: 500);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDtoId>> GetAllCategories()
        {
            try
            {
                return Ok(_categoryService.GetCategories());
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(GetAllCategories)}!", ex.Message, statusCode: 500);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)  
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteCategory)}!", "Category with that id not found!", statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteCategory)}!", ex.Message, statusCode: 500);
            }
        }
    }
}

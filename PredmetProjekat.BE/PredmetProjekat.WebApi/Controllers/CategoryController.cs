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
        public ActionResult<CategoryDto> AddCategory([FromBody]CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _categoryService.AddCategory(categoryDto);
            return CreatedAtAction("AddCategory", new { Id = category.CategoryId }, category);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDtoId>> GetAllCategories()
        {
            return Ok(_categoryService.GetCategories());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<CategoryDtoId>> DeleteCategory([FromRoute] Guid id)  
        {
            //todo logicko brisanje
            return Ok(_categoryService.DeleteCategory(id));
        }
    }
}

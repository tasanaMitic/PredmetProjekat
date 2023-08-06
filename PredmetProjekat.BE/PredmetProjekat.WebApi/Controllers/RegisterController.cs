using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces.IService;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        public RegisterController(IRegisterService categoryService)
        {
            _registerService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<RegisterDto> AddRegister(RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Guid registerId = _registerService.AddRegister(register);
                return CreatedAtAction("AddRegister", new { Id = registerId }, register);
            }
            catch (ArgumentException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddRegister)}!", ex.Message, statusCode: 400);
            }
            catch (DuplicateNameException ex)
            {
                return Problem($"Something went wrong in the {nameof(AddRegister)}!", ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(AddRegister)}!", ex.Message, statusCode: 500);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<RegisterDtoId>> GetAllRegisters()
        {
            try
            {
                return Ok(_registerService.GetRegisters());
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(GetAllRegisters)}!", ex.Message, statusCode: 500);
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRegister(Guid id)
        {
            try
            {
                _registerService.DeleteRegister(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteRegister)}!", "Brand with that id not found!", statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteRegister)}!", ex.Message, statusCode: 500);
            }
        }
    }
}

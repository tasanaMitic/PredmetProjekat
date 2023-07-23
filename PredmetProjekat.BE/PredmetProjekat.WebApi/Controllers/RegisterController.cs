using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
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

        [Authorize]
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
            catch (ArgumentException e)
            {
                return BadRequest();
            }
            catch (DuplicateNameException e)
            {
                return BadRequest("Duplicate name!");   //TODO fix this
            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<RegisterDtoId>> GetAllRegisters()
        {
            return Ok(_registerService.GetRegisters());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteRegister(Guid id)
        {
            return _registerService.DeleteRegister(id) ? (IActionResult)NoContent() : NotFound();
        }
    }
}

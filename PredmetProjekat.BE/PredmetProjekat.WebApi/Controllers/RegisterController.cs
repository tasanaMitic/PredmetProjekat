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
        public ActionResult<RegisterDto> AddRegister([FromBody] RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid registerId = _registerService.AddRegister(register);
            return CreatedAtAction("AddRegister", new { Id = registerId }, register);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public ActionResult<IEnumerable<RegisterDtoId>> GetAllRegisters()
        {
            return Ok(_registerService.GetRegisters());

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRegister([FromRoute] Guid id)
        {
            _registerService.DeleteRegister(id);
            return NoContent();
        }
    }
}

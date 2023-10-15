using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos.UserDtos;
using PredmetProjekat.Common.Interfaces.IService;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService emloyeeService)
        {
            _employeeService = emloyeeService;
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmloyees()
        {
            return Ok(await _employeeService.GetEmloyees());
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<ActionResult<EmployeeDto>> GetEmployee([FromRoute] string username)
        {
            return Ok(await _employeeService.GetEmloyee(username));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteEmloyee([FromRoute] string username)
        {
            try
            {
                return await _employeeService.DeleteEmloyee(username) ? NoContent() : Problem($"Something went wrong in the {nameof(DeleteEmloyee)}!", statusCode: 500);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteEmloyee)}!", ex.Message, statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteEmloyee)}!", ex.Message, statusCode: 500);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IActionResult> AssignManager([FromBody] ManagerDto managerDto)
        {
            try
            {
                return await _employeeService.AssignManager(managerDto) ? Ok() : Problem($"Something went wrong in the {nameof(AssignManager)}!", statusCode: 500); 
            } 
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(AssignManager)}!", ex.Message, statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(AssignManager)}!", ex.Message, statusCode: 500);
            }

        }

        [Authorize(Roles = "Employee")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UserDto userDto)
        {
            try
            {
                return await _employeeService.UpdateEmployee(userDto) ? NoContent() : Problem($"Something went wrong in the {nameof(UpdateEmployee)}!", statusCode: 500);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(UpdateEmployee)}!", ex.Message, statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(UpdateEmployee)}!", ex.Message, statusCode: 500);
            }
        }
    }
}

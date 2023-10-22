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
        [Route("all")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmloyees()
        {
            return Ok(await _employeeService.GetEmloyees());
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("{username}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee([FromRoute] string username)
        {
            return Ok(await _employeeService.GetEmloyee(username));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{username}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> DeleteEmloyee([FromRoute] string username)
        {
            return Ok(await _employeeService.DeleteEmloyee(username));

        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> AssignManager([FromBody] ManagerDto managerDto)
        {
            return Ok(await _employeeService.AssignManager(managerDto));

        }

        [Authorize(Roles = "Employee")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UserDto userDto)
        {
            return await _employeeService.UpdateEmployee(userDto) ? NoContent() : Problem($"Something went wrong in the {nameof(UpdateEmployee)}!", statusCode: 500);
        }
    }
}

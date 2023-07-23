using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _emloyeeService;
        public EmployeeController(IEmployeeService emloyeeService)
        {
            _emloyeeService = emloyeeService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmloyees()
        {
            return Ok(await _emloyeeService.GetEmloyees());

        }

        [Authorize]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteEmloyee(string username)
        {
            return await _emloyeeService.DeleteEmloyee(username) ? (IActionResult)NoContent() : NotFound();
        }

        [Authorize]
        [HttpPut]
        public IActionResult AssignManager(ManagerDto managerDto)   //TODO
        {
            return _emloyeeService.AssignManager(managerDto) ? Ok() : BadRequest();
        }
    }
}

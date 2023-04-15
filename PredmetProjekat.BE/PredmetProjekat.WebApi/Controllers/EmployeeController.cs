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

        [HttpPost]
        public ActionResult<EmployeeDto> AddEmloyee(AccountDto employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                string username = _emloyeeService.AddEmloyee(employee);
                return CreatedAtAction("AddEmloyee", new { Id = username }, employee);
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
            catch (DuplicateNameException e)
            {
                return BadRequest("Duplicate name!");   //TODO
            }
            catch (Exception e)
            {
                return BadRequest(e); ;   //TODO
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmloyees()
        {
            return Ok(_emloyeeService.GetEmloyees());

        }

        [HttpDelete("{username}")]
        public IActionResult DeleteEmloyee(string username)
        {
            return _emloyeeService.DeleteEmloyee(username) ? (IActionResult)NoContent() : NotFound();
        }

        [HttpPut]
        public IActionResult AssignManager(ManagerDto managerDto)
        {
            return _emloyeeService.AssignManager(managerDto) ? Ok() : BadRequest();
        }
    }
}

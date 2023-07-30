﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService emloyeeService)
        {
            _employeeService = emloyeeService;
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmloyees()
        {
            try
            {
                return Ok(await _employeeService.GetEmloyees());
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(GetAllEmloyees)}!", ex.Message, statusCode: 500);
            }


        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteEmloyee(string username)
        {
            try
            {
                return await _employeeService.DeleteEmloyee(username) ? (IActionResult)NoContent() : Problem("Something went wrong!", statusCode: 500);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(AssignManager)}!", ex.Message, statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteEmloyee)}!", ex.Message, statusCode: 500);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IActionResult> AssignManager(ManagerDto managerDto)
        {
            try
            {
                return await _employeeService.AssignManager(managerDto) ? Ok() : Problem("Something went wrong!", statusCode: 500); 
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
        public async Task<IActionResult> UpdateAdmin(UserDto userDto)
        {
            try
            {
                return await _employeeService.UpdateEmployee(userDto) ? NoContent() : Problem("Something went wrong!", statusCode: 500);
            }
            catch (KeyNotFoundException ex)
            {
                return Problem($"Something went wrong in the {nameof(UpdateAdmin)}!", ex.Message, statusCode: 404);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(UpdateAdmin)}!", ex.Message, statusCode: 500);
            }
        }
    }
}

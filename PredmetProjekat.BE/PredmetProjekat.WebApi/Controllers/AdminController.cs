using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAdmins()
        {
            try
            {
                return Ok(await _adminService.GetAdmins());
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(GetAllAdmins)}!", ex.Message, statusCode: 500);
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteAdmin(string username)
        {
            try
            {
                return await _adminService.DeleteAdmin(username) ? (IActionResult)NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(DeleteAdmin)}!", ex.Message, statusCode: 500);
            }

        }
    }
}

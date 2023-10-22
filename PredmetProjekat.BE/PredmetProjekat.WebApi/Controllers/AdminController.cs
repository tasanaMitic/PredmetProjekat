using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos.UserDtos;
using PredmetProjekat.Common.Interfaces.IService;

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
        [Route("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAdmins()
        {
            return Ok(await _adminService.GetAdmins());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetAdmin([FromRoute] string username)
        {
            return Ok(await _adminService.GetAdmin(username));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteAdmin([FromRoute] string username)   //todo not used
        {
            return await _adminService.DeleteAdmin(username) ? NoContent() : Problem($"Something went wrong in the {nameof(DeleteAdmin)}!", statusCode: 500);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateAdmin([FromBody] UserDto userDto)   //todo not used
        {
            return await _adminService.UpdateAdmin(userDto) ? NoContent() : Problem($"Something went wrong in the {nameof(UpdateAdmin)}!", statusCode: 500);
        }
    }
}

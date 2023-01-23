using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using System.Data;

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

        [HttpPost]
        public ActionResult<AccountDto> AddAdmin(AccountDto account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                string username = _adminService.AddAdmin(account);
                return CreatedAtAction("AddAdmin", new { Id = username }, account);
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
        public ActionResult<IEnumerable<AccountDto>> GetAllAdmins()
        {
            return Ok(_adminService.GetAdmins());

        }

        [HttpDelete("{username}")]
        public IActionResult DeleteAdmin(string username)
        {
            return _adminService.DeleteAdmin(username) ? (IActionResult)NoContent() : NotFound();
        }
    }
}

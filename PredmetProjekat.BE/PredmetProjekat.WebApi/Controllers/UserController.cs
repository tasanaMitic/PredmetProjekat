using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<UserDto> AddUser(UserDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                Guid userId = _userService.AddUser(user);
                return CreatedAtAction("AddUser", new { Id = userId }, user);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
            catch (DuplicateNameException e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDtoId>> GetAllUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            return _userService.DeleteUser(id) ? (IActionResult)NoContent() : NotFound();
        }

        [HttpPost]
        public IActionResult AssignManager()
        {
            //todo
            return Ok();
        }

    }
}

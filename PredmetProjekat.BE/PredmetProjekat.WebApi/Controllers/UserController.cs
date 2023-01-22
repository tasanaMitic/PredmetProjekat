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
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
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

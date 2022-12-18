using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using System.Data;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtikalController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ArtikalDto> AddArtikal(ArtikalDto artikal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                //string username = _userService.AddUser(user);
                return CreatedAtAction("AddArtikal", new { Id = "" }, artikal);
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
            catch (DuplicateNameException e)
            {
                return BadRequest();
            }

        }
    }
}

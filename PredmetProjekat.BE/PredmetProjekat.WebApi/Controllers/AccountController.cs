using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;
        public AccountController(UserManager<Account> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] AccountDto accountDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var account = _mapper.Map<Account>(accountDto);
                var result = await _userManager.CreateAsync(account, accountDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(account, accountDto.Role);

                return Ok();
            }
            catch (Exception e)
            {
                return Problem("Something went wrong!", statusCode: 500);
            }            
        }


        //[HttpPost]
        //[Route("login")]
        //public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

        //        if (!result.Succeeded)
        //        {
        //            return Unauthorized(loginDto);
        //        }

        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return Problem("Something went wrong!", statusCode: 500);
        //    }
        //}
    }
}

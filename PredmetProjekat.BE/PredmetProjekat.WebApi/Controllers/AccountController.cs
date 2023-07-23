using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        public AccountController(UserManager<Account> userManager, IMapper mapper, IAuthManager authManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
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

                return Accepted();
            }
            catch (Exception e)
            {
                return Problem($"Something went wrong in the {nameof(Register)}!", e.Message, statusCode: 500);
            }            
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if(!await _authManager.ValidateUser(loginDto))
                {
                    return Unauthorized();
                }


                return Accepted(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(Login)}!", statusCode: 500);
            }
        }
    }
}

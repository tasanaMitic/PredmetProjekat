using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos.IdentityDtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IAccountService _registrationService;
        public AccountController(IAuthManager authManager, IAccountService registrationService)
        {
            _authManager = authManager;
            _registrationService = registrationService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("admin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _registrationService.RegisterAdmin(registrationDto, this.ModelState);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Accepted();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("employee")]
        public async Task<ActionResult> RegisterEmployee([FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _registrationService.RegisterEmployee(registrationDto, this.ModelState);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);//to do throw error
            }

            return Accepted();
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _authManager.ValidateUser(loginDto))
            {
                return Unauthorized();  //to do throw error
            }


            return Accepted(new { Token = await _authManager.CreateToken() });
        }
    }
}

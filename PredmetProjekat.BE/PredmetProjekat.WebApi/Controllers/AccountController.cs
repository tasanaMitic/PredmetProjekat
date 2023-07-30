﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PredmetProjekat.Common.Dtos.IdentityDtos;
using PredmetProjekat.Common.Enums;
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

        [HttpPost]
        [Route("admin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegistrationDto registrationDto)
        {
            try
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
            catch (Exception ex)
            { 
                return Problem($"Something went wrong in the {nameof(RegisterAdmin)}!", ex.Message, statusCode: 500);
            }            
        }

        [HttpPost]
        [Route("employee")]
        public async Task<ActionResult> RegisterEmployee([FromBody] RegistrationDto registrationDto)
        {
            try
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

                    return BadRequest(ModelState);
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(RegisterEmployee)}!", ex.Message, statusCode: 500);
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
                return Problem($"Something went wrong in the {nameof(Login)}!", ex.Message, statusCode: 500);
            }
        }
    }
}

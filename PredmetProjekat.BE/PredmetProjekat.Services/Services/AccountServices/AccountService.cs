using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PredmetProjekat.Common.Dtos.IdentityDtos;
using PredmetProjekat.Common.Enums;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Account> _userManager;
        public AccountService(IMapper mapper, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAdmin(RegistrationDto registrationDto, ModelStateDictionary modelState)
        {
            var account = _mapper.Map<Account>(registrationDto);
            var result = await _userManager.CreateAsync(account, registrationDto.Password);

            if (!result.Succeeded)
            {
                return result;
            }

            var roleResult = await _userManager.AddToRoleAsync(account, UserRole.Admin.ToString());
            if (!roleResult.Succeeded)
            {
                //TODO
                // revert creation
                return roleResult;
            }

            return roleResult;
        }

        public async Task<IdentityResult> RegisterEmployee(RegistrationDto registrationDto, ModelStateDictionary modelState)
        {
            var account = _mapper.Map<Account>(registrationDto);
            var result = await _userManager.CreateAsync(account, registrationDto.Password);

            if (!result.Succeeded)
            {
                return result;
            }

            var roleResult = await _userManager.AddToRoleAsync(account, UserRole.Employee.ToString());
            if (!roleResult.Succeeded)
            {
                //TODO
                // revert creation
                return roleResult;
            }

            return roleResult;
        }

    }
}

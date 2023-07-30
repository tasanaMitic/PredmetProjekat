using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PredmetProjekat.Common.Dtos.IdentityDtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAdmin(RegistrationDto registrationDto, ModelStateDictionary modelState);
        Task<IdentityResult> RegisterEmployee(RegistrationDto registrationDto, ModelStateDictionary modelState);
    }
}

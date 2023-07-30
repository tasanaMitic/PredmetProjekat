using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Enums;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;
        public AdminService(UserManager<Account> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAdmin(string username)
        {
            var userToBeDeleted = await _userManager.FindByNameAsync(username);
            if(userToBeDeleted == null)
            {
                throw new KeyNotFoundException("User not found in the database!");
            }

            var result = await _userManager.DeleteAsync(userToBeDeleted);
            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDto>> GetAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync(UserRole.Admin.ToString());
            return _mapper.Map<IEnumerable<UserDto>>(admins);
        }

        public async Task<bool> UpdateAdmin(UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found in the database!");
            }

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}

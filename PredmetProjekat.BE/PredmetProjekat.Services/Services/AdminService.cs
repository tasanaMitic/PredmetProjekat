using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PredmetProjekat.Common.Constants;
using PredmetProjekat.Common.Dtos.UserDtos;
using PredmetProjekat.Common.Interfaces.IService;
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
                throw new KeyNotFoundException($"User with username: {username} not found in the database!");
            }

            var result = await _userManager.DeleteAsync(userToBeDeleted);
            return result.Succeeded;
        }

        public async Task<UserDto> GetAdmin(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username: {username} not found in the database!");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync(Constants.AdminRole);
            return _mapper.Map<IEnumerable<UserDto>>(admins);
        }

        public async Task<bool> UpdateAdmin(UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username: {userDto.Username} not found in the database!");
            }

            user.FirstName = userDto.FirstName;
            user.Lastname = userDto.LastName;
            user.Email = userDto.Email;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}

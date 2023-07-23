﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PredmetProjekat.Common.Dtos;
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
                return false;
            }

            var result = await _userManager.DeleteAsync(userToBeDeleted);
            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDto>> GetAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            return _mapper.Map<IEnumerable<UserDto>>(admins);
        }
    }
}

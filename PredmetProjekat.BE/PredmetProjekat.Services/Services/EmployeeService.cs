using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork, UserManager<Account> userManager, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public bool AssignManager(ManagerDto managerDto)
        {
            //if (managerDto.ManagerUsername == null)
            //{
            //    return _unitOfWork.EmployeeRepository.RemoveManager(managerDto.EmployeeUsername);
            //}
            //else
            //{
            //    return _unitOfWork.EmployeeRepository.AssignManager(managerDto.ManagerUsername, managerDto.EmployeeUsername);
            //}
            return true;
        }

        public async Task<bool> DeleteEmloyee(string username)
        {
            var userToBeDeleted = await _userManager.FindByNameAsync(username);
            if (userToBeDeleted == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(userToBeDeleted);
            return result.Succeeded;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmloyees()
        {
            var users = await _userManager.GetUsersInRoleAsync("Employee");
            return _mapper.Map<IEnumerable<EmployeeDto>>(users);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Dtos.UserDtos;
using PredmetProjekat.Common.Enums;
using PredmetProjekat.Common.Interfaces.IService;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;
        public EmployeeService(UserManager<Account> userManager, IMapper mapper )
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> AssignManager(ManagerDto managerDto)
        {
            if (managerDto.ManagerUsername == null)
            {
                return await RemoveManager(managerDto.EmployeeUsername);
            }
            else
            {
                return await AssignManager(managerDto.ManagerUsername, managerDto.EmployeeUsername);
            }

        }

        private async Task<bool> AssignManager(string managerUsername, string employeeUsername)
        {
            var manager = (await _userManager.GetUsersInRoleAsync(UserRole.Employee.ToString())).Where(x => x.UserName == managerUsername).FirstOrDefault();
            var employee = (await _userManager.GetUsersInRoleAsync(UserRole.Employee.ToString())).Where(x => x.UserName == employeeUsername).FirstOrDefault();

            if (manager == null)
            {
                throw new KeyNotFoundException($"Manager with username: {managerUsername} not found in the database!");
            }
            else if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with username: {employeeUsername} not found in the database!");
            }
            else if (manager == employee)
            {
                return false;
            }

            employee.Manager = manager;

            var result = await _userManager.UpdateAsync(employee);
            return result.Succeeded;
        }

        private async Task<bool> RemoveManager(string employeeUsername)
        {
            var employee = await _userManager.Users.Include(x => x.Manager).FirstOrDefaultAsync(x => x.UserName == employeeUsername);

            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with username: {employeeUsername} not found in the database!");
            }
            else if (employee.Manager == null)
            {
                throw new KeyNotFoundException($"Employee with username: {employeeUsername} doesn't have a manager!");
            }

            employee.Manager = null;

            var result = await _userManager.UpdateAsync(employee);
            return result.Succeeded;
        }

        public async Task<bool> DeleteEmloyee(string username)
        {
            var user = await _userManager.Users.Include(x => x.Manages).FirstOrDefaultAsync(x => x.UserName == username); 
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username: {username} not found in the database!");
            }
            var managedByUser = user.Manages.ToList();

            if (managedByUser.Count > 0)
            {
                foreach (var employee in managedByUser)
                {
                    employee.Manager = null;
                    await _userManager.UpdateAsync(employee);
                }
            }

            user.IsDeleted = true;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmloyees()
        {
            var employees = (await _userManager.GetUsersInRoleAsync(UserRole.Employee.ToString())).Where(x => !x.IsDeleted).ToList();
            var allUsers = await _userManager.Users.Include(x => x.Manager).ToListAsync();

            var employeesWithManagers = allUsers.Where(x => employees.Any(y => y.UserName == x.UserName)).ToList();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithManagers);
        }

        public async Task<bool> UpdateEmployee(UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username: {userDto.Username} not found in the database!");
            }

            var result = await _userManager.UpdateAsync(_mapper.Map<Account>(userDto));
            return result.Succeeded;
        }
    }
}

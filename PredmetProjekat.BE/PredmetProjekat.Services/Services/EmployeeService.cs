using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string AddEmloyee(AccountDto employeeDto)
        {
            _unitOfWork.EmployeeRepository.Add(new Employee
            {
                Lastname = employeeDto.LastName,
                FirstName = employeeDto.FirstName,
                UserName = employeeDto.Username
            });

            return employeeDto.Username;
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

        public bool DeleteEmloyee(string username)
        {
            //    var employees = _unitOfWork.EmployeeRepository.Find(x => x.ManagerUsername == username);

            //    foreach (var employee in employees)
            //    {
            //        _unitOfWork.EmployeeRepository.RemoveManager(employee.UserName);
            //    }

            //    return _unitOfWork.EmployeeRepository.RemoveByUsername(username);
            return true;
        }

    public IEnumerable<EmployeeDto> GetEmloyees()
        {
            return _unitOfWork.EmployeeRepository.GetAll().Select(x => new EmployeeDto
            {
                LastName = x.Lastname,
                FirstName = x.FirstName,
                Username = x.UserName
                //ManagerUsername = x.ManagerUsername
            });
        }
    }
}

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
                Lastname = employeeDto.Lastname,
                Name = employeeDto.Name,
                Username = employeeDto.Username
            });

            return employeeDto.Username;
        }

        public bool AssignManager(ManagerDto managerDto)
        {
            if (managerDto.ManagerUsername == null)
            {
                return _unitOfWork.EmployeeRepository.RemoveManager(managerDto.EmployeeUsername);
            }
            else
            {
                return _unitOfWork.EmployeeRepository.AssignManager(managerDto.ManagerUsername, managerDto.EmployeeUsername);
            }
            
        }

        public bool DeleteEmloyee(string username)
        {
            var employees = _unitOfWork.EmployeeRepository.Find(x => x.ManagerUsername == username);

            foreach (var employee in employees)
            {
                _unitOfWork.EmployeeRepository.RemoveManager(employee.Username);
            }

            return _unitOfWork.EmployeeRepository.RemoveByUsername(username);
        }

        public IEnumerable<EmployeeDto> GetEmloyees()
        {
            return _unitOfWork.EmployeeRepository.GetAll().Select(x => new EmployeeDto
            {
                Lastname = x.Lastname,
                Name = x.Name,
                Username = x.Username,
                ManagerUsername = x.Manager?.Username
            });
        }
    }
}

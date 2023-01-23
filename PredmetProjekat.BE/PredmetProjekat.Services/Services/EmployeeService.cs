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
        public string AddEmloyee(AccountDto accountDto)
        {
            _unitOfWork.EmployeeRepository.Add(new Employee
            {
                Lastname = accountDto.Lastname,
                Name = accountDto.Name,
                Username = accountDto.Username
            });

            return accountDto.Username;
        }

        public bool DeleteEmloyee(string username)
        {
            return _unitOfWork.EmployeeRepository.RemoveByUsername(username);
        }

        public IEnumerable<AccountDto> GetEmloyees()
        {
            return _unitOfWork.EmployeeRepository.GetAll().Select(x => new AccountDto
            {
                Lastname = x.Lastname,
                Name = x.Name,
                Username = x.Username
            });
        }
    }
}

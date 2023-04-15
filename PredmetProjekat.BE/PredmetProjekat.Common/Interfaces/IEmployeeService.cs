using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IEmployeeService
    {
        string AddEmloyee(AccountDto employeeDto);
        IEnumerable<EmployeeDto> GetEmloyees();
        bool DeleteEmloyee(string username);
        bool AssignManager(ManagerDto managerDto);
    }
}

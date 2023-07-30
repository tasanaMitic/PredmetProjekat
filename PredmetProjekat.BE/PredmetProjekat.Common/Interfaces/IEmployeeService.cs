using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmloyees();
        Task<bool> DeleteEmloyee(string username);
        Task<bool> AssignManager(ManagerDto managerDto);
    }
}

using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmloyees();
        Task<bool> DeleteEmloyee(string username);
        Task<bool> AssignManager(ManagerDto managerDto);
        Task<bool> UpdateEmployee(UserDto useDtos);
    }
}

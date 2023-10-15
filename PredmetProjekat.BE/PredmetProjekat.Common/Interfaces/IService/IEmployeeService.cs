using PredmetProjekat.Common.Dtos.UserDtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmloyees();
        Task<IEnumerable<EmployeeDto>> DeleteEmloyee(string username);
        Task<IEnumerable<EmployeeDto>> AssignManager(ManagerDto managerDto);
        Task<bool> UpdateEmployee(UserDto useDtos);
        Task<EmployeeDto> GetEmloyee(string username);
    }
}

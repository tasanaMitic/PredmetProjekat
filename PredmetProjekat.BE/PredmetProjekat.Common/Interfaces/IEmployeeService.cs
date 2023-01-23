using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IEmployeeService
    {
        string AddEmloyee(AccountDto accountDto);
        IEnumerable<AccountDto> GetEmloyees();
        bool DeleteEmloyee(string username);
    }
}

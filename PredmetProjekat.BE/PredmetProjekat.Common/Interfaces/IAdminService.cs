using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IAdminService
    {
        string AddAdmin(AccountDto accountDto);
        IEnumerable<AccountDto> GetAdmins();
        bool DeleteAdmin(string username);
    }
}

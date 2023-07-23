using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<UserDto>> GetAdmins();
        Task<bool> DeleteAdmin(string username);
    }
}

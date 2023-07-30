using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IAdminService
    {
        Task<bool> DeleteAdmin(string username);
        Task<IEnumerable<UserDto>> GetAdmins();
    }
}

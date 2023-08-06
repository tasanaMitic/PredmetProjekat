using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IAdminService
    {
        Task<bool> DeleteAdmin(string username);
        Task<IEnumerable<UserDto>> GetAdmins();
        Task<bool> UpdateAdmin(UserDto userDto);
    }
}

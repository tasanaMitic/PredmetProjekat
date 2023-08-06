using PredmetProjekat.Common.Dtos.UserDtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IAdminService
    {
        Task<bool> DeleteAdmin(string username);
        Task<IEnumerable<UserDto>> GetAdmins();
        Task<bool> UpdateAdmin(UserDto userDto);
    }
}

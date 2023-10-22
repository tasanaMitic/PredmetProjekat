using PredmetProjekat.Common.Dtos.UserDtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IAdminService
    {
        Task<bool> DeleteAdmin(string username);
        Task<IEnumerable<UserDto>> GetAdmins();
        Task<UserDto> GetAdmin(string username);
        Task<bool> UpdateAdmin(UserDto userDto);
    }
}

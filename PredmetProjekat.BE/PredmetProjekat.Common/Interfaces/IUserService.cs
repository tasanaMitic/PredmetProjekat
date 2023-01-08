using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IUserService
    {
        Guid AddUser(UserDto userDto);
        IEnumerable<UserDto> GetUsers();
        UserDtoId GetUser(Guid id);
        bool DeleteUser(Guid id);

    }
}

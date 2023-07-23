using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDto loginDto);
        Task<string> CreateToken();
    }
}

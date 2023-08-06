using PredmetProjekat.Common.Dtos.IdentityDtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDto loginDto);
        Task<string> CreateToken();
        string DecodeToken(string tokenString);
    }
}

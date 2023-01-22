using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IRegisterService
    {
        Guid AddRegister(RegisterDto registerDto);
        IEnumerable<RegisterDto> GetRegisters();
        bool DeleteRegister(Guid id);
    }
}

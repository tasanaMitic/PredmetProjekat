using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IRegisterService
    {
        Guid AddRegister(RegisterDto registerDto);
        IEnumerable<RegisterDtoId> GetRegisters();
        void DeleteRegister(Guid id);
    }
}

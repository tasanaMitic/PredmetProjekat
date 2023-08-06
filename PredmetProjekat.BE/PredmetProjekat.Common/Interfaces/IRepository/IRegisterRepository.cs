using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface IRegisterRepository : IGenericRepository<Register>
    {
        IEnumerable<Register> GetAllRegisters();
        Register GetRegisterById(Guid registerId);
        void CreateRegister(Register register);
        void DeleteRegister(Register register);
    }
}

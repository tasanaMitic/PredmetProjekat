using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    public class RegisterRepository : GenericRepository<Register>, IRegisterRepository
    {
        public RegisterRepository(StoreContext context) : base(context)
        {
        }

        public void CreateRegister(Register register)
        {
            Create(register);
        }

        public void DeleteRegister(Register register)
        {
            Delete(register);
        }

        public IEnumerable<Register> GetAllRegisters()
        {
            return GetAll();
        }

        public Register GetRegisterById(Guid registerId)
        {
            return GetById(registerId);
        }
    }
}

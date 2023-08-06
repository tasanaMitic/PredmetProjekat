using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        void UpdateAccount(Account account);
    }
}

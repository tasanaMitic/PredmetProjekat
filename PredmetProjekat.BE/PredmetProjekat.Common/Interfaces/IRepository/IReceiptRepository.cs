using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        IEnumerable<Receipt> GetAllReceipts();
        IEnumerable<Receipt> GetFilteredSales(IEnumerable<string> employeeUsernames, IEnumerable<string> registerCodes, IEnumerable<string> locations, string startDate, string endDate, decimal? price);
        IEnumerable<Receipt> GetAllReceiptsForUser(Account user);
        Receipt GetReceiptById(Guid receiptId);
        void CreateReceipt(Receipt receipt);
        void DeleteReceipt(Receipt receipt);
    }
}

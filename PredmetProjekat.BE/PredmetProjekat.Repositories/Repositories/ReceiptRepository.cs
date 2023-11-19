using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    public class ReceiptRepository : GenericRepository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(StoreContext context) : base(context)
        {

        }
        public void CreateReceipt(Receipt receipt)
        {
            Create(receipt);
        }

        public void DeleteReceipt(Receipt receipt)
        {
            Delete(receipt);
        }

        public IEnumerable<Receipt> GetAllReceipts()
        {
            return _context.Receipts.Include(x => x.SoldProducts).Include(x => x.SoldBy).ToList();
        }

        public IEnumerable<Receipt> GetAllReceiptsForUser(Account user)
        {
            return _context.Receipts.Where(x => x.SoldBy == user).Include(x => x.SoldProducts).ToList();
        }

        public Receipt GetReceiptById(Guid receiptId)
        {
            return _context.Receipts.Where(x => x.ReceiptId == receiptId).Include(x => x.SoldProducts).Include(x => x.SoldBy).FirstOrDefault();
        }
    }
}

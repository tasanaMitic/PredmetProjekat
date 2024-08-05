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
            return _context.Receipts.Include(x => x.SoldProducts)
                                        .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.ProductType)
                                    .Include(x => x.SoldBy)
                                    .Include(x => x.Register)
                                    .ToList();
        }

        public IEnumerable<Receipt> GetAllReceiptsForUser(Account user)
        {
            return _context.Receipts.Where(x => x.SoldBy == user)
                                    .Include(x => x.SoldProducts)
                                        .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.ProductType)
                                    .Include(x => x.Register)
                                    .ToList();
        }

        public IEnumerable<Receipt> GetFilteredSales(IEnumerable<string> employeeUsernames, IEnumerable<string> registerCodes, IEnumerable<string> locations, string startDate, string endDate, decimal? price)
        {

            return _context.Receipts.Include(x => x.SoldProducts)
                                        .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.ProductType)
                                    .Include(x => x.SoldBy)
                                    .Include(x => x.Register)
                                        .Where(x => employeeUsernames.Contains(x.SoldBy.UserName) || employeeUsernames == null)
                                        .Where(x => registerCodes.Contains(x.Register.RegisterCode) || registerCodes == null)
                                        .Where(x => x.TotalPrice <= price || price == null)
                                        .Where(x => locations.Contains(x.Register.Location) || locations == null)
                                        .Where(x => (startDate == null || x.Date.Date >= DateTime.Parse(startDate).Date) && (endDate == null || x.Date.Date <= DateTime.Parse(endDate).Date))
                                        .ToList();
        }

        public Receipt GetReceiptById(Guid receiptId)
        {
            return _context.Receipts.Where(x => x.ReceiptId == receiptId)
                                    .Include(x => x.SoldProducts)
                                    .Include(x => x.SoldBy)
                                    .Include(x => x.Register)
                                    .FirstOrDefault();
        }
    }
}

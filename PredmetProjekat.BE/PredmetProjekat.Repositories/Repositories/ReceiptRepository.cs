using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;
using System.Globalization;

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

        public IEnumerable<Receipt> GetFilteredSales(IEnumerable<string> employeeUsernames, IEnumerable<string> saleDates, IEnumerable<string> registerCodes)
        {
            var allReceipts = _context.Receipts.Include(x => x.SoldProducts)
                                                    .ThenInclude(x => x.Product)
                                                    .ThenInclude(x => x.ProductType)
                                                .Include(x => x.SoldBy)
                                                .Include(x => x.Register)
                                                .ToList();

            var filterByUsernames = employeeUsernames != null
                ? allReceipts.Where(x => employeeUsernames.Contains(x.SoldBy.UserName)).ToList()
                : new List<Receipt>();

            var filterByDates = saleDates != null
                ? allReceipts.Where(x => saleDates.Contains(x.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))).ToList()
                : new List<Receipt>();

            var filterByCodes = registerCodes != null
                ? allReceipts.Where(x => registerCodes.Contains(x.Register.RegisterCode)).ToList()
                : new List<Receipt>();

            var filtered = (employeeUsernames != null ? filterByUsernames : allReceipts)
                            .Intersect(saleDates != null ? filterByDates : allReceipts)
                            .Intersect(registerCodes != null ? filterByCodes : allReceipts)
                            .ToList();

            
            return filtered;
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

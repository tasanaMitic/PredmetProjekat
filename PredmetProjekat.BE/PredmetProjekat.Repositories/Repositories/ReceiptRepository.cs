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
            return _context.Receipts.Include(x => x.SoldProducts).ThenInclude(x => x.Product).ThenInclude(x => x.ProductType).Include(x => x.SoldBy).Include(x => x.Register).ToList();
        }

        public IEnumerable<Receipt> GetAllReceiptsForUser(Account user)
        {
            return _context.Receipts.Where(x => x.SoldBy == user).Include(x => x.SoldProducts).ThenInclude(x => x.Product).ThenInclude(x => x.ProductType).Include(x => x.Register).ToList();
        }

        public IEnumerable<Receipt> GetFilteredSales(IEnumerable<string> employeeUsernames, IEnumerable<string> saleDates, IEnumerable<string> registerCodes)
        {
            var allReceipts = _context.Receipts.Include(x => x.SoldProducts).ThenInclude(x => x.Product).ThenInclude(x => x.ProductType).Include(x => x.SoldBy).Include(x => x.Register).ToList();

            var filterByUsernames = new List<Receipt>();
            var filterByDates = new List<Receipt>();
            var filterByCodes = new List<Receipt>();
            if (employeeUsernames != null)
            {
                filterByUsernames = allReceipts.Where(x => employeeUsernames.Contains(x.SoldBy.UserName)).ToList();
            }

            if (saleDates != null)
            {
                filterByDates = allReceipts.Where(x => saleDates.Contains(x.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))).ToList();
            }

            if (registerCodes != null)
            {
                filterByCodes = allReceipts.Where(x => registerCodes.Contains(x.Register.RegisterCode)).ToList();
            }

            var filtered = filterByCodes.Union(filterByDates).Union(filterByUsernames).ToList();
            return filtered.Count > 0 ? filtered : allReceipts;
        }

        public Receipt GetReceiptById(Guid receiptId)
        {
            return _context.Receipts.Where(x => x.ReceiptId == receiptId).Include(x => x.SoldProducts).Include(x => x.SoldBy).Include(x => x.Register).FirstOrDefault();
        }
    }
}

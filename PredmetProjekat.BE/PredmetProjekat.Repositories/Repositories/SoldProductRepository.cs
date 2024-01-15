using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;
using System.Collections.Generic;

namespace PredmetProjekat.Repositories.Repositories
{
    public class SoldProductRepository : GenericRepository<SoldProduct>, ISoldProductRepository
    {
        public SoldProductRepository(StoreContext context) : base(context) { }
        public void CreateSoldProduct(SoldProduct soldProduct)
        {
            Create(soldProduct);
        }

        public IEnumerable<SoldProduct> GetSoldProductsByIds(IEnumerable<Guid> soldproductsIds)
        {
            return _context.SoldProducts.Where(x => soldproductsIds.Contains(x.SoldProductId)).ToList();
        }
    }
}

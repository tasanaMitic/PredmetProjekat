using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }
    }
}

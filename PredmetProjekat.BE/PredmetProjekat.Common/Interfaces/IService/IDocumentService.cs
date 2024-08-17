using PredmetProjekat.Common.Dtos.ProductDtos;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IDocumentService
    {
        void CreatePDF(IEnumerable<Receipt> sales, FilterParams filterParams, string username);
    }
}

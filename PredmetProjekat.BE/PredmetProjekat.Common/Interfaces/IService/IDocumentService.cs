
using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IDocumentService
    {
        void CreatePDF(List<LineItem> lineItems);
    }
}

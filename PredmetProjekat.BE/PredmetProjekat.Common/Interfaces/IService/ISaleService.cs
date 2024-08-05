using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Dtos.ProductDtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface ISaleService
    {
        void CreatePDF(FilterParams filterParams, string username);
        void SellProduct(SaleDto saleDto, string username);
        IEnumerable<ReceiptDto> GetAllSalesForUser(string username);
        IEnumerable<ReceiptDto> GetAllSales();
        FilterSearchDto GetFilteredSales(FilterParams filterParams, string username);
    }
}

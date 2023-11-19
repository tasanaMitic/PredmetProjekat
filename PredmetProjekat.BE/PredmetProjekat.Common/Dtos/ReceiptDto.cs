using PredmetProjekat.Common.Dtos.ProductDtos;
using PredmetProjekat.Common.Dtos.UserDtos;

namespace PredmetProjekat.Common.Dtos
{
    public class ReceiptDto
    {
        public Guid ReceiptId { get; set; }
        public DateTime Date { get; set; }
        public RegisterDtoId Register { get; set; }
        public EmployeeDto SoldBy { get; set; }
        public IEnumerable<SoldProductDto> SoldProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
